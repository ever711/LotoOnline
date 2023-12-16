using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LoDeOnline.Models;
using LoDeOnline.Services;
using MyERP.Services;
using LoDeOnline.Domain;
using LoDeOnline.Data;
using System.Collections.Generic;

namespace LoDeOnline.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly AccountJournalService _journalService;
        private readonly ResBankService _bankService;
        private readonly AccountPaymentService _paymentService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly PartnerService _partnerService;
        private readonly DanhDeLineService _danhDeLineService;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            AccountJournalService journalService,
            ResBankService bankService,
            AccountPaymentService paymentService,
            IUnitOfWorkAsync unitOfWork,
            PartnerService partnerService,
            DanhDeLineService danhDeLineService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _journalService = journalService;
            _bankService = bankService;
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _partnerService = partnerService;
            _danhDeLineService = danhDeLineService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var banks = _bankService.Search(x => x.Active == true).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                Banks = banks,
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return Json(new {
                    success = true,
                    message = "Đổi mật khẩu thành công."
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                message = string.Join("; ", result.Errors)
            }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult UserInfo()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            var credit = _partnerService.CreditDebitGet(new List<long>() { user.PartnerId })[user.PartnerId].Credit;
            var model = new UserInfoViewModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                Credit = -credit / 1000
            };
            return PartialView(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NapTien(NapTienViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = GetModelStateError(ModelState),
                });
            }

            var user = _userManager.FindById(User.Identity.GetUserId());
            var payment = new AccountPayment
            {
                JournalId = model.JournalId,
                Amount = model.Amount,
                CompanyId = user.CompanyId,
                PaymentType = "inbound",
                CurrencyId = user.Company.CurrencyId,
                PartnerType = "customer",
                PartnerId = user.PartnerId,
                PaymentDate = DateTime.Today,
                Communication = model.Sender + (!string.IsNullOrEmpty(model.TransactionCode) ? " - " + model.TransactionCode : ""),
            };

            try
            {
                _unitOfWork.BeginTransaction();
                _paymentService.Insert(payment);
                _unitOfWork.Commit();

                return Json(new
                {
                    success = true,
                    message = "Nạp tiền thành công",
                });
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                return Json(new
                {
                    success = false,
                    message = "Nạp tiền thất bại, thử lại sau.",
                });
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public ActionResult GetSoDanh(long danh_de_id)
        {
            var models = _danhDeLineService.Search(x => x.DanhDeId == danh_de_id).Select(x => new GetSoDanhViewModel
            {
                LoaiDeName = x.LoaiDe.Name,
                PriceSubtotal = x.PriceSubtotal,
                PriceUnit = x.PriceUnit,
                Quantity = x.Quantity,
                SoDanh = x.SoDanh
            });
            return PartialView(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RutTien(RutTienViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = GetModelStateError(ModelState),
                });
            }

          
            if (model.Amount <= 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Số tiền rút phải lớn hơn 0",
                });
            }

            var user = _userManager.FindById(User.Identity.GetUserId());

            var code_phone = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber.Substring(user.PhoneNumber.Length - Math.Min(user.PhoneNumber.Length, 5), Math.Min(user.PhoneNumber.Length, 5)) : "";
            if (model.Last5PhoneNumber != code_phone)
            {
                return Json(new
                {
                    success = false,
                    message = "Mã 5 số điện thoại không đúng",
                });
            }

            var credit_virtual = -_partnerService.CreditAvailable(new List<long>() { user.PartnerId })[user.PartnerId].VirtualAvailable;
            if (credit_virtual < model.Amount)
            {
                return Json(new
                {
                    success = false,
                    message = string.Format("Bạn chỉ có thể rút tối đa {0}", Math.Max(0, credit_virtual)),
                });
            }

            var payment = new AccountPayment
            {
                JournalId = model.JournalId,
                Amount = model.Amount,
                CompanyId = user.CompanyId,
                PaymentType = "outbound",
                CurrencyId = user.Company.CurrencyId,
                PartnerType = "customer",
                PartnerId = user.PartnerId,
                PaymentDate = DateTime.Today,
                Communication = model.Receiver + " - " + model.AccNumber,
            };

            try
            {
                _unitOfWork.BeginTransaction();
                _paymentService.Insert(payment);
                _unitOfWork.Commit();

                return Json(new
                {
                    success = true,
                    message = "Rút tiền thành công",
                });
            }
            catch
            {
                _unitOfWork.Rollback();
                return Json(new
                {
                    success = false,
                    message = "Rút tiền thất bại, thử lại sau.",
                });
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        //
        // POST: /Manage/LinkLogin
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LinkLogin(string provider)
        //{
        //    // Request a redirect to the external login provider to link a login for the current user
        //    return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        //}

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}