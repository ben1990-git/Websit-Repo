using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Intrfaces;
using WebSite.ViewModels;

namespace WebSite.Service
{
    public class UserModelService : IUserModelService
    {
        public UserViewModel UserToVm(User user,int id)
        {
            if (user!=null)
            {
                UserViewModel vm = new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    UserName = user.UserName,
                    Password = user.Password,
                    RetypePassword = user.Password,
                    Id = id

                };
                return vm;
            }
            return null;
      
        }

        public User VMToUser(UserViewModel vm , int id)
        {
            if (vm != null)
            {
                User user = new User()
                {
                    FirstName=vm.FirstName,
                    LastName = vm.LastName,
                    BirthDate = vm.BirthDate,
                    Email = vm.Email,
                    UserName = vm.UserName,
                    Password = vm.Password,                   
                    Id = id,

                };
                return user;
            }
            return null;
        }
    }
}
