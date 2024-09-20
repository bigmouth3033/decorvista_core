using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWizWebApp.Interfaces;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAdminController : ControllerBase
    {
        private IProductAdmin _productAdmin;

        public ProductAdminController(IProductAdmin productAdmin)
        {
            _productAdmin = productAdmin;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("create_product")]
        public async Task<IActionResult> CreateProduct([FromForm]RequestCreateNewProduct requestCreateNewProduct)
        {
            var customResult = await _productAdmin.CreateNewProduct(requestCreateNewProduct);
            return Ok(customResult);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("get_products")]
        public async Task<IActionResult> GetProductList([FromQuery] int pageNumber, [FromQuery] int pageSize )
        {
            var customPaging = await _productAdmin.GetProductList(pageNumber, pageSize);
            return Ok(customPaging);
        }
    }
}
