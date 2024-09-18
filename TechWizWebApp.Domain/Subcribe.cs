using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorVista.Domain
{
    public class Subcribe
    {
        [Key]
        public int id { get; set; }
        public int gallery_id { get; set; }
        public Gallery gallery { get; set; } = new Gallery();
        public int user_id { get; set; }
        public User user { get; set; } = new User();
    }
}
