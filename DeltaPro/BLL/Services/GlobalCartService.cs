using BLL.Interfaces;
using DAL.DataBase;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
   public class GlobalCartService : IGlobalCart
    {        
        public List<Product> Cart { get ;  set; }
        public bool GotDbInfo { get ; set; }

        IHttpContextAccessor _httpContextAccessor;       
        public GlobalCartService( IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;           
        }
        public List<Product> GetAveilableProducts()
        {
            var cartString = _httpContextAccessor.HttpContext.Session.GetString("cart");
            var cart = new List<Product>();

            if (!string.IsNullOrEmpty(cartString))
            {
                var productsidsstring = cartString.Split('-');
                foreach (var item in Cart)
                {
                    if (!productsidsstring.Contains(item.Id.ToString()))
                    {
                        cart.Add(item);
                    }
                }
                
            }
            else
            {
                cart = Cart;
            }

            return cart;
        }

      
    }
}
