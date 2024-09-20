using DecorVista.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private IGallery _gallery;
        public GalleryController(IGallery gallery)
        {
            _gallery = gallery;
        }

        [HttpPost("createNew")] 
        public async Task<ActionResult> Create([FromForm] Gallery gallery)
        {
            var result = await _gallery.Create(gallery);
            if(result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetAll")]

        public async Task<ActionResult> GetAll()
        {
            var result = await _gallery.GetAll();
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
        public async Task<ActionResult> ChangeStatus(int id)
        {
            var result = await _gallery.ChangeStatus(id);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _gallery.GetById(id);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] Gallery e)
        {
            var result = await _gallery.Update(e);
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
