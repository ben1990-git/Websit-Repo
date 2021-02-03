using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Validation;

namespace WebSite.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " Please enter your first name ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = " Please enter your last name ")]
        public string LastName { get; set; }

        [Display(Name = " Birthdate")]
        [Required(ErrorMessage = " Please  your date of birth ")]
        [DataType(DataType.Date)]

        public DateTime BirthDate { get; set; }

        [Display(Name = "EMail Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        [Required(ErrorMessage = " Please enter your email ")]
        public string Email { get; set; }

        [Display(Name = "User Name:")]
        [Required(ErrorMessage = " Please enter your User name ")]
        [UsernameValidationAttribute(ErrorMessage = "User name all ready in use ")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = " Please enter your password ")]
        [StringLength(10)]

        [Compare(nameof(RetypePassword), ErrorMessage = " Passwords don't match ")]
        public string Password { get; set; }

        [Display(Name = " Retype password")]
        public string RetypePassword { get; set; }
    }
}
