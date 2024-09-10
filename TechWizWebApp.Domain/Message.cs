using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechWizWebApp.Domain
{
    public class Message
    {
        public int Id { get; set; }

        public string? MessageContent {  get; set; } 

        public int CustomerId { get; set; }

        public string From { get; set; } // Admin, Customer

        public User? Customer { get; set; }
    }
}
