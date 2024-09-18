using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatClientController : ControllerBase
    {
        //private readonly IChatClient _chatClient;

        //public ChatClientController(IChatClient chatClient)
        //{
        //    _chatClient = chatClient;
        //}

        //[HttpPost]
        //[Authorize]
        //[Route("save_message")]
        //public async Task<IActionResult> SaveMessage([FromForm]RequestSaveMessage requestSaveMessage)
        //{
        //    var customResult = await _chatClient.SaveSendToAdminMessage(requestSaveMessage.Id, requestSaveMessage.Message);

        //    return Ok(customResult);
        //}
    }
}
