using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechWizWebApp.Domain
{
    public class Gallery
    {
        [Key]
        public int id { get; set; }
        public string gallery_name { get; set; }= string.Empty;
        public string description { get; set; } = string.Empty;
        public bool status { get; set; }
        public int room_type_id { get; set; }
        public string imageName { get; set; } = string.Empty;
        public RoomType room_type { get; set; } = new RoomType();
        public List<GalleryDetails> galleryDetails { get; set; } = new List<GalleryDetails>();
        public List<Subcribe> subcribes { get; set; } = new List<Subcribe>();        
        [NotMapped]
        public List<IFormFile>? uploadImages { get; set; }
    }
}
