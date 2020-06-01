using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ARoomInterior.Models
{
    public class IndexViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Register.EmailError), ErrorMessageResourceType = typeof(Resources.Register))]
        [Display(Name = "Email", ResourceType = typeof(Resources.ManageAccount))]
        public string Email { get; set; }
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Register))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.NameMessage), MinimumLength = 4)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Speciallization", ResourceType = typeof(Resources.ManageAccount))]
        public string Speciallization { get; set; }
        [Required]
        [Display(Name = "Info", ResourceType = typeof(Resources.ManageAccount))]
        public string Info { get; set; }
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
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
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.SetPassword), ErrorMessageResourceName = nameof(Resources.SetPassword.PasswordError), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.SetPassword), Name = nameof(Resources.SetPassword.Password))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.SetPassword), Name = nameof(Resources.SetPassword.ConfirmPassword))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.SetPassword), ErrorMessageResourceName = nameof(Resources.SetPassword.ConfirmPasswordError))]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.PasswordMessage), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Register))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.PasswordMessage), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.ManageAccount))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.PasswordMatchMessage))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Register))]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}