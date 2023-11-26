using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using signalr_and_quartz.Models;

namespace signalr_and_quartz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto order)
        {
            var orderDb = new Order();

            orderDb.Id = Guid.NewGuid().ToString();
            orderDb.Adress = order.Adress;
            orderDb.ExpectedDate = order.ExpectedDate;
            orderDb.IsDeliverd = order.IsDeliverd;

            await _context.Orders.AddAsync(orderDb);
            await _context.SaveChangesAsync();
            return Ok(orderDb);
        }

        [HttpPut("DeliverOrder")]
        public async Task<IActionResult> DeliverOrder([FromBody]string id)
        {
            var order =  _context.Orders.SingleOrDefault(o=>o.Id==id);
            if (order == null)
                return BadRequest("no ordr with this id");
            if (order.IsDeliverd)
                return BadRequest("order is already Deliverd");

            order.IsDeliverd = true;
            await _context.SaveChangesAsync();
            return Ok(order);
        }


    }
}
