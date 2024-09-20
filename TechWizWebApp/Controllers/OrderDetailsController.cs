using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWizWebApp.Data;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly DecorVistaDbContext _context;
        public OrderDetailsController(DecorVistaDbContext decorVistaDbContext)
        {
            _context = decorVistaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetailsByOrderId(string orderId)
        {
            var orderDetails =await _context.OrderDetails
                .Include(od => od.order)
                .Include(od => od.variant)
                .Where(od => od.order_id == orderId)
                
                .ToListAsync();
            return Ok(new CustomResult
            {
                data = orderDetails,
                Message = "oke",
                Status = 200
            });
        }
    }
}
