using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class OrderModel
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long CakeId{ get; set; }
        public long CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsPaid { get; set; }

}
}
