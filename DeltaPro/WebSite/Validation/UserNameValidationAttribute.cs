using WebSite.ViewModels;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebSite.Validation
{
    public class UsernameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            WebSiteDBContext db = (WebSiteDBContext)validationContext.GetService(typeof(WebSiteDBContext));

            var currentUser = (UserViewModel)validationContext.ObjectInstance;

            var UserInDb = db.Users.Where(x => x.UserName == currentUser.UserName).FirstOrDefault();
            if (UserInDb != null)
            {
                return new ValidationResult("Sorry,User name is in use");
            }

            return ValidationResult.Success;
        }
    }
}
