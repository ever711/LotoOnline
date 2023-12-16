using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LoDeOnline.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> Banks { get; set; }
    }

    public class NapTienViewModel
    {
        public long BankId { get; set; }

        public long JournalId { get; set; }

        public string Sender { get; set; }

        public decimal Amount { get; set; }

        public string TransactionCode { get; set; }
    }


    public class GetSoDanhViewModel
    {
        public string SoDanh { get; set; }

        public decimal PriceUnit { get; set; }

        public decimal Quantity { get; set; }

        public decimal? PriceSubtotal { get; set; }

        public string LoaiDeName { get; set; }
    }

    public class RutTienViewModel
    {
        public long BankId { get; set; }

        public long JournalId { get; set; }

        public string Receiver { get; set; }

        public string AccNumber { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// 5 số điện thoại cuối
        /// </summary>
        public string Last5PhoneNumber { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// K
        /// </summary>
        public decimal? Credit { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu hiện tại.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới.")]
        [StringLength(100, ErrorMessage = "Mật khẩu mới phải tối đa {0} ký tự và tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và xác thực mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}