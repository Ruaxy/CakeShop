using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.MQ.Events
{
    public class EventTemplate
    {
        public string Message { get; set; }
        public string TableName { get; set; }
    }
}
