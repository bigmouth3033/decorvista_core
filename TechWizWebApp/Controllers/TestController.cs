using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechWizWebApp.Services;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;
        private readonly ISeedService _seedService;

        public TestController(IFileService fileService,IMailService mailService, ISeedService seedService)
        {
            _fileService = fileService;
            _mailService = mailService;
            _seedService = seedService;
        }

        [HttpPost("testUploadFile")]
        public async Task<IActionResult> TestUploadImage(IFormFile file)
        {
            var result = await _fileService.UploadImageAsync(file);
            return Ok(result);
        }

        [HttpPost("TestSendMail")]
        public async  Task<IActionResult> TestSendMail(string emailReceiver, string subject, string message)
        {
            _mailService.SendMailAsync(emailReceiver, subject, message);
            return Ok("");
        }

        [HttpGet("TestSeedProduct")]
        public IActionResult TestSeedProduct()
        {
            _seedService.SeedProduct();
            return Ok("");
        }
        

    }
}
