using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebSite.ViewModels;

namespace WebSite.Controllers
{
    public class AuthController : Controller
    {
        IUserService _userService;
        IGlobalCart _globalCart;
        public AuthController(IUserService userService , IGlobalCart globalCart)
        {
            _userService = userService;
            _globalCart = globalCart;
        }

        [HttpGet]
        public IActionResult _Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult _Login(UserLoginViewModel vm)
        {
           
            if (ModelState.IsValid)
            {
                var Token = _userService.Login(vm.UserName, vm.Password);

                if (!string.IsNullOrEmpty(Token))
                {
                    HttpContext.Session.Remove("cart");
                    _userService.UserAuthraztion(Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("User", "User Not found");
                }
                
            }
            return View("~/Views/Home/Index.cshtml", _globalCart.Cart);
            
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("cart");
             HttpContext.Response.Cookies.Delete("UserCookie");
            return RedirectToAction("Index", "Home");
        }
     
    }
}
