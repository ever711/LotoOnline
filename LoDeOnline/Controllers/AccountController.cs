using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyERP.Services;
using LoDeOnline.Models;
using LoDeOnline.Domain;
using LoDeOnline.Services;
using LoDeOnline.Data;
using System.Collections.Generic;

namespace LoDeOnline.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly PartnerService _partnerService;
        private readonly CompanyService _companyService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IRModelDataService _modelDataService;
        private readonly ResGroupService _resGroupService;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            PartnerService partnerService,
            IUnitOfWorkAsync unitOfWork,
            CompanyService companyService,
            IRModelDataService modelDataService,
            ResGroupService resGroupService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _partnerService = partnerService;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
            _modelDataService = modelDataService;
            _resGroupService = resGroupService;
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
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // 
        // POST: /Account/Login 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout 
            // To enable password failures to trigger account lockout, change to shouldLockout: true 
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAjax(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user.Active == false)
            {
                return Json(new
                {
                    success = false,
                    message = "Tài khoản của bạn không còn hiệu lực.",
                }, JsonRequestBehavior.AllowGet);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Json(new
                    {
                        success = true,
                        message = "",
                    }, JsonRequestBehavior.AllowGet);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    return Json(new
                    {
                        success = false,
                        message = "Email hoặc mật khẩu không đúng",
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        // 
        // POST: /Account/Register 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BeginTransaction();
                    var company = _companyService.GetCurrentCompany();

                    var partner = new Partner
                    {
                        Name = model.Email,
                        Email = model.Email,
                        Phone = model.PhoneNumber,
                        Customer = true,
                        CompanyId = company.Id
                    };
                    _partnerService.Insert(partner);

                    var group_member = _resGroupService.GetById(((ResGroup)_modelDataService.GetRef("base.group_member")).Id);
                    var user = new ApplicationUser
                    {
                        Name = model.Email,
                        UserName = model.Email,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        PartnerId = partner.Id,
                        EmailConfirmed = true,
                        CompanyId = company.Id,
                        Groups = new List<ResGroup>() { group_member }
                    };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        _unitOfWork.Commit();
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771 
                        // Send an email with this link 
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id); 
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme); 
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>"); 
                        //Assign Role to user Here    
                        //await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                        //Ends Here  
                        return Json(new
                        {
                            success = true,
                            message = "Đăng ký tài khoản thành công"
                        });
                    }

                    _unitOfWork.Rollback();
                    AddErrors(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    _unitOfWork.Rollback();
                }
            }

            // If we got this far, something failed, redisplay form 
            return Json(new
            {
                success = false,
                message = GetModelStateError(ModelState)
            });
            //return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Users");
        }
    }
}