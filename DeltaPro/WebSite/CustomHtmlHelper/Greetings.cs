using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.CustomHtmlHelper
{
    public static class Greetings
    {
        public static string Greet(this IHtmlHelper htmlHelper, string UserName)
        {
            string Greet = string.Empty;
            DateTime currentDateTime = DateTime.Now;
            if (currentDateTime.Hour >= 0 && currentDateTime.Hour <= 7)
            {
                Greet = "Good night " + UserName;
            }
            if (currentDateTime.Hour > 7 && currentDateTime.Hour <= 14)
            {
                Greet = "Good morning " + UserName;
            }
            if (currentDateTime.Hour > 14 && currentDateTime.Hour <= 20)
            {
                Greet = "Good afternoon " + UserName;
            }
            if (currentDateTime.Hour > 20 && currentDateTime.Hour <= 23)
            {
                Greet = "Good evening " + UserName;
            }

            return Greet;
        }
    }
}
