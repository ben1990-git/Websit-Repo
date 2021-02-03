using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebSite.Intrfaces;
using WebSite.ViewModels;

namespace WebSite.Controllers
{
    public class HomeController :Controller
    {
        IUserService _userService;

        IProductService _productService;

        IUserModelService _userModelService;

        IGlobalCart _globalCart;
        public HomeController(IUserService userService, IProductService productService, IUserModelService userModelService , IGlobalCart globalCart)
        {
            _userService = userService;
            _productService = productService;
            _userModelService = userModelService;
            _globalCart = globalCart;
            _globalCart.Cart = _productService.GetProducts();
        }
        public async Task<IActionResult> Index(string orderby)
        {
            var list = await Task.Run (()=>_globalCart.GetAveilableProducts().Where(p => p.UserId == null).ToList());

            if (orderby=="Date")
            {
              list=  _productService.OrderByDate(list).ToList();
            }
            if (orderby=="Name")
            {
              list=  _productService.OrderByName(list).ToList();
            }
            
           
          
            return View(list);

        }

        [HttpGet]
        public  IActionResult SignUp()
        {
            return  View();
        }

        [HttpPost]
        public  IActionResult SignUp( UserViewModel vm)
        {
         

            if (ModelState.IsValid)
            {
                var NewUser = new User()
                {
                    UserName = vm.UserName,
                    Password = vm.Password,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    BirthDate = vm.BirthDate.Date,
                    Email = vm.Email,
                };
                if (!_userService.AddNewUser(NewUser))
                {
                    ModelState.AddModelError("NewErorr", "UserName Allready in use");
                    return View(vm);
                }

                return RedirectToAction("Index");
            }
            else
            {
                
                return View(vm);
            }
            
        }

        [HttpGet]        
        public   IActionResult Update()
        {
            var id = _userService.ReadUserCookie();
            var result = _userService.GetRegisterUser(id);
            var NewUVM = _userModelService.UserToVm(result, id);
            return View(NewUVM);
        
        }

        [HttpPost]
        public IActionResult Update(UserViewModel vm)
        {
            ModelState.Remove("UserName");
            if (ModelState.IsValid)
            {
                var id = _userService.ReadUserCookie();
                var result = _userModelService.VMToUser(vm, id);
                _userService.UpdateUserDetails(result);
                return RedirectToAction("Index");

            }
            else
            {
                return View(vm);
            }
          
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult AddNewProduct()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "User")]
        public  IActionResult AddNewProduct(ProductViewModel vm)
        {
            ViewBag.Headline = new string("add new product");

            if (ModelState.IsValid)
            {
                var NewProduct = new Product();
                NewProduct.Title = vm.title;
                NewProduct.Date = vm.Date;
                NewProduct.ShortDiscription = vm.ShortDiscription;
                NewProduct.LongDiscription = vm.LongDiscription;
                NewProduct.Price = vm.Price;
                NewProduct.Picture1 = _productService.AddProductPictures(vm.Picture1);
                NewProduct.Picture2 = _productService.AddProductPictures(vm.Picture2);
                NewProduct.Picture3 = _productService.AddProductPictures(vm.Picture3);
                NewProduct.OwnerId = _userService.ReadUserCookie();

                var Result = _productService.AddProductToDB(NewProduct);
                _globalCart.Cart.Add(NewProduct);

                ViewData.Add("Aprove", Result);
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            var result = _productService.GetProduct(id);
            var Owner = _userService.GetRegisterUser((int)result.OwnerId);
            if (result!=null)
            {
                return View(result);
            }
            else
            {
                Response.StatusCode = 404;
                return View("Custom404Page");
            }
        }

        [HttpGet]
        public IActionResult AddToUserCart(int id)
        {                     
            var result = _userService.AddProductToUserCart(id);                      
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ShopingCart()
        {

            
           var UserCart =  _userService.GetUserCart(_globalCart.Cart);
            return View(UserCart);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFormUserCart(int id)
        {
           bool result = await Task.Run(()=> _userService.RemoveProductFromUserCart(id));
            return RedirectToAction("ShopingCart");
        }

        [HttpGet]
        public async Task<IActionResult> ChackOut()
        {
            var list = _userService.GetUserCart(_globalCart.Cart);
          await  _userService.ChackOut(list);
            return RedirectToAction("Index");
        }
    }
}
