using DAL.DataBase;
using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        WebSiteDBContext _ctx;

        public ProductRepository( WebSiteDBContext ctx)
        {
            _ctx = ctx;
        }

        public bool Add(Product entity)
        {
            if (entity != null)
            {
                _ctx.Products.Add(entity);
                return true;
            }
            else
                return false;
        }

        public IEnumerable<Product> Get()
        {
            if (_ctx.Products != null)
            {
                return _ctx.Products.ToList();
            }
            else
                return null;
            
        }

        public Product Get(int id)
        {
            return _ctx.Products.FirstOrDefault(u => u.Id == id);
        }

        public  bool Remove(int id)
        {

            if (Get(id) != null)
            {
                _ctx.Products.Remove(Get(id));
                
                return true;
            }
            else return false;
        }

        public async Task  Save()
        {
          await  _ctx.SaveChangesAsync();
            _ctx.Dispose();
        }

        public bool Update(Product entity)
        {
            if (Get(entity.Id) != null)
            {

                var OrderInDb = Get(entity.Id);
                var OrderEntry = _ctx.Entry(OrderInDb);
                OrderEntry.CurrentValues.SetValues(entity);
                return true;
            }
            else return false;

        }
    }
}
