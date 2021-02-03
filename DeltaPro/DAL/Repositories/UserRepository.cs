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
    public class UserRepository : IRepository<User>
    {
        WebSiteDBContext _ctx;
        public UserRepository( WebSiteDBContext ctx)
        {
            _ctx = ctx;
        }
        public bool Add(User entity)
        {
            if (entity != null)
            {
                _ctx.Users.Add(entity);
                return true;
            }
            else
                return false;
        }
        public IEnumerable<User> Get()
        {
           return _ctx.Users.ToList();
        }
        public User Get(int id)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
        }
        public bool Remove(int id)
        {
            if (Get(id) != null)
            {
                _ctx.Users.Remove(Get(id));
                return true;
            }
            else return false;
            
        }

        public async Task  Save()
        {
          await  _ctx.SaveChangesAsync();
            _ctx.Dispose();
        }
        public bool Update(User entity)
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
