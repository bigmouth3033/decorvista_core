using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _order.GetAll();
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("GetByMonth")]
        public async Task<ActionResult> GetByMonth([FromForm] OrderForm o)
        {
            var result = await _order.getByMonth(o.Month);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("ChangeStatus/{id}")]
        public async Task<ActionResult> ChangeStatus (string id)
        {
            var result = await _order.ChangeStatus(id);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        } 
        public class OrderForm
        {
         

            public DateTime Month { get; set; }
        }
    }
}
