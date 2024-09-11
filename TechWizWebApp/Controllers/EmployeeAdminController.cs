using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWizWebApp.Interfaces;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAdminController : ControllerBase
    {
        private readonly IEmployeeAdmin _employeeAdmin;

        public EmployeeAdminController(IEmployeeAdmin employeeAdmin)
        {
            _employeeAdmin = employeeAdmin;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create_employee")]
        public async Task<IActionResult> CreateNewEmployee([FromForm] RequestCreateEmployee requestCreateEmployee)
        {
            var customResult = await _employeeAdmin.CreateNewEmployee(requestCreateEmployee);

            return Ok(customResult);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get_employee")]
        public async Task<IActionResult> GetAllEmployee([FromQuery] int pageNumber, [FromQuery] int pageSize )
        {
            var customPaging = await _employeeAdmin.GetAllEmployee(pageNumber,pageSize);

            return Ok(customPaging);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("change_active")]
        public async Task<IActionResult> ChangeEmployeeActive([FromForm] int userId)
        {

            var customResult = await _employeeAdmin.ChangeEmployeeActive(userId);

            return Ok(customResult);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("change_permission")]
        public async Task<IActionResult> ChangeEmployeePermission([FromForm] RequestPermission requestPermission )
        {
 

            var customResult = await _employeeAdmin.ChangeEmployeePermission(requestPermission.UserId, requestPermission.PermissionName);

            return Ok(customResult);
        }



    }
}
