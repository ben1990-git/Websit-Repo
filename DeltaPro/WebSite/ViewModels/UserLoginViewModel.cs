using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = " *Please enter your User name")]


        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = " *Please enter your password")]
        [StringLength(10)]


        public string Password { get; set; }
    }
}
