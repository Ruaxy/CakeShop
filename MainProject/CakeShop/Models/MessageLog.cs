using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class MessageLog
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string TableName { get; set; }
    }
}
