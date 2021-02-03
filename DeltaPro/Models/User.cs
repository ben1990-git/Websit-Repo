using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICollection<Product> ProductsToSell { get; set; }
        public ICollection<Product> ProductsToBuy { get; set; }
    }
}
