using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWizWebApp.Data;
using TechWizWebApp.Domain;

namespace TechWizWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly DecorVistaDbContext _context;
        public GalleryController(DecorVistaDbContext decorVistaDbContext)
        {
            _context = decorVistaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetGalleryByProductId(int productId)
        {
            var product = await _context.Products
                .Include(p => p.galleryDetails)
                .ThenInclude(gd => gd.gallery)
                .Where(p => p.id == productId)
                .FirstOrDefaultAsync();


            if (product?.galleryDetails == null)
            {
                return Ok(new CustomResult
                {
                    data = null,
                    Message = "nothing",
                    Status = 400
                });
            }


            var products = await _context.Products
                .Include(p => p.functionality)
                .Include(p => p.galleryDetails)
                .ThenInclude(gd => gd.gallery)
                .Where(p => p.galleryDetails.Any(gd => gd.gallery_id == product.galleryDetails[0].gallery_id && gd.product_id != productId))
                .ToListAsync();


            return Ok(new CustomResult
            {
                data = products,
                Message = "OK",
                Status = 200
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateGallery()
        {
            Gallery gallery = new Gallery();
            gallery.gallery_name = "Summer";
            gallery.description = "Bo suu tap cho mua he";
            gallery.status = true;
            gallery.room_type_id = 1;
            gallery.color_tone = "Red";
            gallery.view_count = 5252;
            gallery.imageName = "00152aff-f675-4fa2-bb0a-cef158ea4fc7.png";

            int[] productIds = [1, 2, 3];
            List<GalleryDetails> galleriesDetails = new List<GalleryDetails>();
            foreach (int item in productIds)
            {
                GalleryDetails galleryDetails = new GalleryDetails();
                galleryDetails.product_id = item;
                galleryDetails.gallery = gallery;
                galleriesDetails.Add(galleryDetails);
            }

            _context.AddRange(galleriesDetails);
            _context.SaveChanges();
            return Ok("");
        }
    }
    public class CustomResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object? data { get; set; }
    }

}
