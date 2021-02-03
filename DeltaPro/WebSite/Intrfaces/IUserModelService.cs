using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.ViewModels;

namespace WebSite.Intrfaces
{
    public interface IUserModelService
    {
        User VMToUser(UserViewModel vm, int id);
        UserViewModel UserToVm(User user ,int id);
    }
}
