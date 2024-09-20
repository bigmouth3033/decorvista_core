using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetails _orderDetails;
        public OrderDetailController(IOrderDetails orderDetails)
        {
            _orderDetails   = orderDetails;
        }
        [HttpGet("GetByOrderId/{orderId}")]
        public async Task<ActionResult> GetByOrderId (string orderId)
        {
            var result = await _orderDetails.GetByOrderId(orderId);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
