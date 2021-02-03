using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class Product
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string ShortDiscription { get; set; }
        public string LongDiscription { get; set; }
        public DateTime Date { get; set; }

        public decimal Price { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }

        public int State { get; set; }
        public User Owner { get; set; }
        public User Buyer { get; set; }
    }
}
