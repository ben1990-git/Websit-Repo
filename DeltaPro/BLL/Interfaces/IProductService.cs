using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public  interface IProductService
    {
        byte[] AddProductPictures(IFormFile file);
        Task<bool> AddProductToDB(Product product);
        Product GetProduct(int id);       
        List<Product> GetProducts();
        IEnumerable<Product> OrderByName(List<Product> list);
        IEnumerable<Product> OrderByDate(List<Product> list);


    }
}
