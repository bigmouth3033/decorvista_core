using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecorVista.Domain
{
    public class Order
    {
        public string id { get; set; } = string.Empty;
        public int total { get; set; }
        public int user_id { get; set; }
        public User user { get; set; } = new User();

    }
}
