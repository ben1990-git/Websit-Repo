using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
  public  interface IUserService
    {
        bool HasCookie();
        public int ReadUserCookie();
        User GetRegisterUser(int id);
        bool AddNewUser(User user);
        bool UpdateUserDetails(User user);
        bool AddProductToUserCart(int id);
        bool RemoveProductFromUserCart(int id);
        public string Login(string name, string password);
        List<Product> GetUserCart(List<Product> products);
        Task ChackOut(List<Product> purchase);
        void UserAuthraztion(string Token);

    }
}
