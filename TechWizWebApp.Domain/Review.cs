using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorVista.Domain
{
    public class Review
    {
        [Key]        
        public int id { get; set; }
        public int user_id { get; set; }
        public User user { get; set; } = new User();
        public int product_id { get; set; }
        public Product product { get; set; } = new Product();
        public int designer_id { get; set; }
        public InteriorDesigner? interiordesigner { get; set; } = new InteriorDesigner();
        public string comment { get; set; } = string.Empty;
    }
}
