using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechWizWebApp.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? FullName {  get; set; }

        public string? Avatar {  get; set; }

        public string? PhoneNumber {  get; set; }

        public string? Gender { get; set; }

        public DateTime? Dob { get; set; }

        public string Role { get; set; } // Admin, Customer, Employee

        public bool IsEmailConfirmed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();


        






    }
}
