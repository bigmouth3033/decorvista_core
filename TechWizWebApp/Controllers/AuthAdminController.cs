using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWizWebApp.Interfaces;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAdminController : ControllerBase
    {
        private readonly IAuthAdmin _authAdmin;

        public AuthAdminController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpPost]
        [Route("admin_login")]
        public async Task<IActionResult> AdminLogin([FromForm] RequestLogin requestLogin)
        {

            var customResult = await _authAdmin.AdminLogin(requestLogin.Email, requestLogin.Password);

            return Ok(customResult);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAdmin()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            int.TryParse(idClaim, out int userId);

            var customResult = await _authAdmin.GetAdmin(userId);

            return Ok(customResult);
        }

    }
}
