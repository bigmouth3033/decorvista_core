using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorVista.Domain
{
    public class Consultation
    {
        [Key]        
        public int id { get; set; }
        public int user_id { get; set; }
        public User user { get; set; } = new User();
        public int designer_id { get; set; }
        public InteriorDesigner interior_designer { get; set; } = new InteriorDesigner();
        public string scheduled_datetime { get; set; } = string.Empty;
        public int status { get; set; }
        public string notes { get; set; } = string.Empty;
    }
}
