using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class CartModel
    {
        public long Id { get; set; }
        public long CakeId { get; set; }
        public string UserIdentification { get; set; }
    }
}
