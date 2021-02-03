using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
  public  interface IGlobalCart
    {
        List<Product> Cart { get; set; }

        public bool GotDbInfo { get; set; }

        List<Product> GetAveilableProducts();

        
    }
}
