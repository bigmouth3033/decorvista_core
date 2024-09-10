using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechWizWebApp.Data;
using TechWizWebApp.Domain;
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
            try
            {
                var customResult = await _authAdmin.AdminLogin(requestLogin.Email, requestLogin.Password);

                return Ok(customResult);
            }
            catch (Exception ex)
            {
                return Ok(new CustomResult(400, "Bad Request", ex.Message));
            }
        }


        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdmin()
        {
            try
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
                int.TryParse(idClaim, out int userId);

                var customResult = await _authAdmin.GetAdmin(userId);

                 return Ok(customResult);
            }
            catch (Exception ex)
            {
                return Ok(new CustomResult(400, "Bad Request", ex.Message));
            }
        }



    }
}
