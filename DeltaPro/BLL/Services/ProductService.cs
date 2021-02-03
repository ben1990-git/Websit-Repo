using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public class ProductService:IProductService
    {
        IRepository<Product> _productRepository;
        
        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
           
        }
        public byte[] AddProductPictures(IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    return memoryStream.ToArray();

                }
            }
            return null;
        }
        public async Task<bool> AddProductToDB(Product product)
        {
            var Result= _productRepository.Add(product);            
            if (Result)
            {
              await  _productRepository.Save();
                return Result;
            }
            else
                return Result;
            

        }
        public Product GetProduct (int id)
        {
            return _productRepository.Get(id);           
        }
        public List<Product> GetProducts()
        {
            return _productRepository.Get().Where(p => p.State != 3).ToList();
        }

        public IEnumerable<Product>  OrderByDate(List<Product> list)
        {
            var NewList = list.OrderBy(p => p.Date);
            return NewList;
        }

        public IEnumerable<Product> OrderByName(List<Product> list)
        {
            var NewList = list.OrderBy(p => p.Title);
            return NewList;
        }
    }
}
