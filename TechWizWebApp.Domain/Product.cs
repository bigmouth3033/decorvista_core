using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TechWizWebApp.Domain
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string product_code { get; set; } = string.Empty;
        public string productname { get; set; } = string.Empty;
        public int functionality_id { get; set; }
        public Functionality? functionality { get; set; }
        public string brand { get; set; } = string.Empty;
        public float price { get; set; }
        public string description { get; set; } = string.Empty;
        public string imageName { get; set; } = string.Empty;
        public bool status { get; set; }
        public List<Review>? reviews { get; set; }
        public List<GalleryDetails>? galleryDetails { get; set; } 
        public List<Cart>? carts { get; set; }
        public List<Variant>? variants { get; set; } 
        public List<ProductImage>? images { get; set; } 
        [NotMapped]
        public List<IFormFile>? uploadImages { get; set; }


        [NotMapped]
        public string _productCode
        {
            get
            {
                return GetProductCode();
            }
        }
        public string GetProductCode()
        {
            string productCode = $"P_{functionality?.name}_{brand}_{id}";
            return productCode;
        }
    }
}
