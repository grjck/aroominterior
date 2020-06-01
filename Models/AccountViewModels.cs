using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ARoomInterior.Models
{
    // Модели, возвращаемые действиями AccountController.
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Register.EmailError), ErrorMessageResourceType = typeof(Resources.Register))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Register))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Register))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.NameMessage), MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Speciallization", ResourceType = typeof(Resources.Register))]
        public string Speciallization { get; set; }

        [Required]
        [Display(Name = "Info", ResourceType = typeof(Resources.Register))]
        public string Info { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Register.EmailError), ErrorMessageResourceType = typeof(Resources.Register))]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources.Login))]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Login))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Login))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Register.EmailError), ErrorMessageResourceType = typeof(Resources.Register))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Register))]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.PasswordMessage), MinimumLength = 6)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Register))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.PasswordMatchMessage))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Register))]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Register))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Register), ErrorMessageResourceName = nameof(Resources.Register.NameMessage), MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Speciallization", ResourceType = typeof(Resources.Register))]
        public string Speciallization { get; set; }

        [Required]
        [Display(Name = "Info", ResourceType = typeof(Resources.Register))]
        public string Info { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Register.EmailError), ErrorMessageResourceType = typeof(Resources.Register))]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Register.EmailError), ErrorMessageResourceType = typeof(Resources.Register))]
        [Display(Name = "Email", ResourceType = typeof(Resources.Login))]
        public string Email { get; set; }
    }
}
