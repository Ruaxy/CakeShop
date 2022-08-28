using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class CakeWithTypeModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public long TypeId { get; set; }
        public string OwnerSpecification { get; set; }
        public string TypeName { get; set; }

        public string ImageUrl { get; set; }
    }
}
