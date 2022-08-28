using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TAI_RLaba_PNakielny.Data;
using TAI_RLaba_PNakielny.Models;
using TAI_RLaba_PNakielny.MQ;
using TAI_RLaba_PNakielny.Resources;
using TAI_RLaba_PNakielny.Services;

namespace TAI_RLaba_PNakielny.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CakeAPI : Controller
    {
        private readonly CakesAPPContext _context;
        private readonly Producer _messageProducer;

        public CakeAPI(CakesAPPContext context, Producer MsgProducer)
        {
            _context = context;
            _messageProducer = MsgProducer;
        }
        [HttpGet]
        [Authorize]
        public async Task<List<CakeModel>> GetAsync()
        {
            var cakes = await _context.Cakes.ToListAsync();
            return cakes;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<CakeModel> GetAsync(int id)
        {
            var cake = await _context.Cakes.SingleOrDefaultAsync(x => x.Id == id);
            return cake;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CakeModel request)
        {
            var newCake = new CakeModel
            {
                Name = request.Name,
                Description = request.Description,
                Size = request.Size,
                Price = request.Price,
                TypeId = request.TypeId,
                OwnerSpecification = request.OwnerSpecification
            };
            await _context.Cakes.AddAsync(newCake);
            await _context.SaveChangesAsync();

            var @event = new NewCake
            {
                Message = "New cake added with name: " + request.Name + "and id: " + newCake.Id.ToString(),
                TableName = "Cake" 
            };
            await _messageProducer.PublishAsync(@event);

            return StatusCode((int)HttpStatusCode.Created, new { newCake.Id });
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(long id, [FromBody] CakeModel request)
        {
            var cake = await _context.Cakes.FindAsync(id);
            if (cake == null)
            {
                return NotFound();
            }

            cake.Name = request.Name;
            cake.Description = request.Description;
            cake.Size = request.Size;
            cake.Price = request.Price;
            cake.TypeId = request.TypeId;
            cake.OwnerSpecification = request.OwnerSpecification;

            _context.Cakes.Update(cake);
            await _context.SaveChangesAsync();

            var @event = new UpdateCake
            {
                Message = "Cake updated with name: " + cake.Name + "and id: " + cake.Id.ToString(),
                TableName = "Cake"
            };
            await _messageProducer.PublishAsync(@event);

            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var cakeToDelete = await _context.Cakes.SingleOrDefaultAsync(x => x.Id == id);
            _context.Cakes.Remove(cakeToDelete);
            await _context.SaveChangesAsync();

            var @event = new DeletedCake
            {
                Message = "Cake deleted with name: " + cakeToDelete.Name + "and id: " + cakeToDelete.Id.ToString(),
                TableName = "Cake"
            };
            await _messageProducer.PublishAsync(@event);

            return Ok();
        }
    }
}
