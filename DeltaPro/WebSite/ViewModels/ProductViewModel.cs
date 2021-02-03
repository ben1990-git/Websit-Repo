using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = " *Please enter Titel!")]
        public string title { get; set; }

        [Display(Name = "short discription")]
        [Required(ErrorMessage = " *Please enter short discription ")]
        public string ShortDiscription { get; set; }

        [Display(Name = "Long discription")]
        [Required(ErrorMessage = " *Please enter Long discription")]
        public string LongDiscription { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Price")]
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = " *Please enter Price")]

        public decimal Price { get; set; }

        public int State { get; set; }

        public IFormFile Picture1 { get; set; }
        public IFormFile Picture2 { get; set; }
        public IFormFile Picture3 { get; set; }

        public ProductViewModel()
        {
            var dateTime = DateTime.Now;
            Date = dateTime.Date;
             
        }
    }
}
