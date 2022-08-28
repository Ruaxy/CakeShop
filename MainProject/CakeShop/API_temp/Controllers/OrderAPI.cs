using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAI_RLaba_PNakielny.Data;
using TAI_RLaba_PNakielny.Models;
using TAI_RLaba_PNakielny.MQ;
using TAI_RLaba_PNakielny.MQ.Events;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TAI_RLaba_PNakielny.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPI : Controller
    {
        private readonly CakesAPPContext _context;
        private readonly Producer _messageProducer;
        public OrderAPI(CakesAPPContext context, Producer MsgProducer)
        {
            _context = context;
            _messageProducer = MsgProducer;
        }
        [HttpGet("{id}")]
        public async Task<OrderModel> GetAsync(int id)
        {
            var cake = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            return cake;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderModel request)
        {
            var newOrder = new OrderModel
            {
                OrderId = request.OrderId,
                CakeId = request.CakeId,
                CustomerId = request.CustomerId,
                Quantity = request.Quantity
            };
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            var @event = new NewOrder
            {
                Message = "New order added with id: " + request.Id + "and Orderid: " + request.OrderId.ToString(),
                TableName = "Order"
            };
            await _messageProducer.PublishAsync(@event);

            return StatusCode((int)HttpStatusCode.Created, new { newOrder.Id });
        }
    }
}
