using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class CartCakeNameModel
    {
        public long Id { get; set; }
        public long CakeId { get; set; }
        public string UserIdentification { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
