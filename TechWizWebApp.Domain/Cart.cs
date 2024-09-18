using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorVista.Domain
{
    public class Cart
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public User user { get; set; } = new User();
        public int product_id { get; set; }

        public Product product { get; set; } = new Product();
        public int quanity { get; set; }
    }
}
