using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWizWebApp.Interfaces;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignerAdminController : ControllerBase
    {
        private readonly IDesignerAdmin _designerAdmin;

        public DesignerAdminController(IDesignerAdmin designerAdmin)
        {
            _designerAdmin = designerAdmin;
        }

        [HttpPost]
        [Route("designer_register")]
        public async Task<IActionResult> DesignerRegister(RequestDesignerRegister requestDesignerRegister)
        {
            var customResult = await _designerAdmin.DesignerRegister(requestDesignerRegister);

            return Ok(customResult);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("pending_list")]
        public async Task<IActionResult> GetPendingApprovedDesigner([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var customPaging = await _designerAdmin.GetListPendingDesigner(pageNumber, pageSize);

            return Ok(customPaging);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("approved_list")]
        public async Task<IActionResult> GetApprovedDesigner([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var customPaging = await _designerAdmin.GetListApprovedDesigner(pageNumber, pageSize);

            return Ok(customPaging);
        }

        [Authorize(Roles = "admin, designer")]
        [HttpGet]
        [Route("get_designer")]
        public async Task<IActionResult> GetDesignerById([FromQuery] int designerId)
        {
            var customResult = await _designerAdmin.GetDesignerById(designerId);

            return Ok(customResult);
        }

        [Authorize(Roles = "designer")]
        [HttpGet]
        [Route("designer_profile")]
        public async Task<IActionResult> GetProfile()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            int.TryParse(idClaim, out int userId);

            var customResult = await _designerAdmin.GetDesignerById(userId);

            return Ok(customResult);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("approve_designer")]
        public async Task<IActionResult> ApproveDesigner([FromForm] int designerId)
        {
            var customResult = await _designerAdmin.ApproveDesigner(designerId);

            return Ok(customResult);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("deny_designer")]
        public async Task<IActionResult> DenyDesigner([FromForm] int designerId)
        {
            var customResult = await _designerAdmin.DenyDesigner(designerId);

            return Ok(customResult);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("change_status")]
        public async Task<IActionResult> ChangeDesignerStatus([FromForm] int designerId)
        {
            var customResult = await _designerAdmin.ChangeDesignerStatus(designerId);
            
            return Ok(customResult);
        }

        [Authorize(Roles = "designer")]
        [HttpPut]
        [Route("update_info")]
        public async Task<IActionResult> ChangeDesignerInfo([FromForm] RequestUpdateDesignerInfo requestUpdateDesignerInfo)
        {
            var customResult = await _designerAdmin.ChangeDesignerInfo(requestUpdateDesignerInfo);

            return Ok(customResult);
        }

        [Authorize(Roles = "designer")]
        [HttpPut]
        [Route("update_dow")]
        public async Task<IActionResult> ChangeDow([FromForm] string dow)
        {

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            int.TryParse(idClaim, out int userId);

            var customResult = await _designerAdmin.ChangeDow(userId, dow);

            return Ok(customResult);
        }

        [Authorize(Roles = "designer")]
        [HttpPut]
        [Route("update_portfolio")]
        public async Task<IActionResult> ChangePortfolio([FromForm] string portfolio)
        {

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            int.TryParse(idClaim, out int userId);

            var customResult = await _designerAdmin.ChangePortfolio(userId, portfolio);

            return Ok(customResult);
        }


        [Authorize(Roles = "designer")]
        [HttpPut]
        [Route("update_image")]
        public async Task<IActionResult> ChangeAvatar([FromForm] IFormFile avatar)
        {

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            int.TryParse(idClaim, out int userId);

            var customResult = await _designerAdmin.UpdateImage(userId, avatar);

            return Ok(customResult);
        }
    }
}
