using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DecorVista.Domain
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string product_code { get; set; } = string.Empty;
        public string productname { get; set; } = string.Empty;
        public int functionality_id { get; set; }
        public Functionality functionality { get; set; } = new Functionality();
        public string brand { get; set; } = string.Empty;
        public float price { get; set; }
        public string description { get; set; } = string.Empty;
        public string imageName { get; set; } = string.Empty;
        public bool status { get; set; }
        public List<Review> reviews { get; set; } = new List<Review>();
        public List<GalleryDetails> galleryDetails { get; set; } = new List<GalleryDetails>();
        public List<Cart> carts { get; set; } = new List<Cart>();
        [NotMapped]
        public List<IFormFile>? uploadImages { get; set; }
    }
}
