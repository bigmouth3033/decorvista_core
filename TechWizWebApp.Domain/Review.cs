using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechWizWebApp.Domain
{
    public class Review
    {
        [Key]        
        public int id { get; set; }
        public int user_id { get; set; }
        public User? user { get; set; }
        public int? product_id { get; set; }
        public Product? product { get; set; }
        public int? designer_id { get; set; }
        public InteriorDesigner? interiordesigner { get; set; } 
        public float? score  { get; set; }
        public string comment { get; set; } = string.Empty;
        public float? score { get; set; }
    }
}
