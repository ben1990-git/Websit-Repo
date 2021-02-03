using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IRepository<User> _userRepository;
        IConfiguration _config;
        IHttpContextAccessor _httpContextAccessor;
        IProductService _productService;
        IGlobalCart _globalCart;

        public UserService(IRepository<User> userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor, IProductService productService, IGlobalCart globalCart)
        {
            _userRepository = userRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _globalCart = globalCart;


        }
        public bool AddNewUser(User user)
        {
            if (!UserNameInUse(user.UserName))
            {
                var result = _userRepository.Add(user);
                _userRepository.Save();
                return result;


            }
            else return false;

        }
        public User GetRegisterUser(int id)
        {
            var RegistedUser = _userRepository.Get(id);
            return RegistedUser;

        }
        public bool UpdateUserDetails(User user)
        {
            if (user != null)
            {
                var Result = _userRepository.Update(user);
                if (Result)
                {
                    _userRepository.Save();
                }
                return Result;
            }
            return false;

        }
        public string Login(string name, string password)
        {
            var selectedUser = _userRepository.Get().FirstOrDefault(u => u.UserName == name && u.Password == password);
            if (selectedUser != null)
            {
                var signingKey = Convert.FromBase64String(_config["Jwt:SigningSecret"]);
                var expirationInMinutes = Convert.ToDouble(_config["Jwt:ExpirationInMinutes"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = null,
                    Audience = null,
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                    Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                    {
                        new Claim("userId", selectedUser.Id.ToString()),

                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityTokenObj = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityTokenObj);

                return token;
            }



            return string.Empty;
        }
        private bool UserNameInUse(string username)
        {
            var Result = _userRepository.Get().FirstOrDefault(u => u.UserName == username);
            if (Result != null)
            {
                return true;
            }
            else return false;
        }
        public bool HasCookie()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["UserCookie"] != null)
            {
                return true;
            }
            return false;
        }
        public int ReadUserCookie()
        {
            if (HasCookie())
            {
                var CookieToken = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Token").Value;
                if (!string.IsNullOrEmpty(CookieToken))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var tol = handler.ReadToken(CookieToken) as JwtSecurityToken;
                    var CorrentUserId = tol.Claims.First(claim => claim.Type == "userId").Value;
                    return int.Parse(CorrentUserId);
                }
            }
            return 0;

        }
        public bool AddProductToUserCart(int id)
        {
            var ProductToAdd = _productService.GetProduct(id);
            var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
            if (ProductToAdd != null)
            {
                if (HasCookie())
                {
                    ProductToAdd.UserId = ReadUserCookie();
                    _userRepository.Save();

                }
                if (!string.IsNullOrEmpty(cart))
                {

                    cart += $"-{id}";
                    _httpContextAccessor.HttpContext.Session.Remove("cart");
                    _httpContextAccessor.HttpContext.Session.SetString("cart", cart);
                    return true;
                }
                else
                {

                    _httpContextAccessor.HttpContext.Session.Remove("cart");
                    _httpContextAccessor.HttpContext.Session.SetString("cart", id.ToString());
                    return true;
                }
            }
            return false;


        }
        public bool RemoveProductFromUserCart(int id)
        {
            var ProductToAdd = _productService.GetProduct(id);

            if (HasCookie())
            {
                ProductToAdd.UserId = null;
                _userRepository.Save();

            }

            var UserCart = _httpContextAccessor.HttpContext.Session.GetString("cart");

            var ProductId = UserCart.Split('-');

            if (ProductToAdd != null)
            {
                if (!string.IsNullOrEmpty(UserCart))
                {

                    for (int i = 0; i < ProductId.Length; i++)
                    {
                        if (int.Parse(ProductId[i]) == id)
                        {
                            ProductId[i] = null;
                        }
                    }

                    var FirstElement = string.Empty;

                    foreach (var item in ProductId)
                    {
                        if (item != null)
                        {
                            FirstElement = item.ToString();
                            break;
                        }
                    }

                    var cart = FirstElement;

                    foreach (var item in ProductId)
                    {
                        if (item != null)
                        {
                            cart += $"-{item}";
                        }
                    }

                    _httpContextAccessor.HttpContext.Session.Remove("cart");
                    _httpContextAccessor.HttpContext.Session.SetString("cart", cart);
                }

            }
            return false;
        }
        public List<Product> GetUserCart(List<Product> products)
        {
            if (HasCookie())
            {
                SetCart();
            }

            var UserCart = new List<Product>();
            var cart = _httpContextAccessor.HttpContext.Session.GetString("cart");
            if (!string.IsNullOrEmpty(cart))
            {
                var UserProducts = cart.Split('-');
                foreach (var item in products)
                {
                    if (item != null)
                    {
                        if (UserProducts.Contains(item.Id.ToString()))
                        {
                            UserCart.Add(item);
                        }
                    }

                }
            }
            return UserCart;
        }
        public void SetCart()
        {
            var UserCart = _httpContextAccessor.HttpContext.Session.GetString("cart");

            if (string.IsNullOrEmpty(UserCart))
            {
                var SavedIds = _productService.GetProducts().Where(p => p.UserId == ReadUserCookie()).Select(p => p.Id).ToList();
                if (SavedIds.Count != 0)
                {
                    var firstElement = SavedIds.First();
                    var cart = firstElement.ToString();
                    foreach (var item in SavedIds)
                    {
                        if (item != firstElement)
                        {
                            cart += $"-{item}";
                        }

                    }
                    _httpContextAccessor.HttpContext.Session.Remove("cart");
                    _httpContextAccessor.HttpContext.Session.SetString("cart", cart);
                }

            }
        }
        public void UserAuthraztion(string Token)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim("Token",Token),
                    };

                var Identity = new ClaimsIdentity(claims, "Claim");
                var userprincable = new ClaimsPrincipal(new[] { Identity });
                _httpContextAccessor.HttpContext.SignInAsync(userprincable, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(1),

                });
            }
        }
        public async Task ChackOut(List<Product> purchase)
        {

            foreach (var item in purchase)
            {
                if (HasCookie())
                {
                    item.Buyer = GetRegisterUser(ReadUserCookie());
                }
                item.State = 3;
            }
          await  _userRepository.Save();
        }
    }
}

