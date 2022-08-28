using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;
using CakeShop.MQ;
using CakeShop.MQ.Events;


namespace CakeShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderAPI : Controller
    {
        private readonly CakesAPPContext _context;
        private readonly Producer _messageProducer;
        public OrderAPI(CakesAPPContext context, Producer MsgProducer)
        {
            _context = context;
            _messageProducer = MsgProducer;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<OrderModel> GetAsync(int id)
        {
            var cake = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            return cake;
        }
        [Authorize]
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

            var @event = new NewCake
            {
                Message = "New order added with id: " + request.Id + " and Orderid: " + request.OrderId.ToString(),
                TableName = "Order"
            };
            await _messageProducer.PublishAsync(@event);

            return StatusCode((int)HttpStatusCode.Created, new { newOrder.Id });
        }
    }
}
