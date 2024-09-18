using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DecorVista.Domain
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public UserDetails userdetails { get; set; } = new UserDetails();
        public InteriorDesigner interiordesigner { get; set; } = new InteriorDesigner();

        public List<Consultation> consultations { get; set; } = new List<Consultation>();

        public List<Review> reviews { get; set; } = new List<Review>();
        public List<Cart> carts { get; set; } = new List<Cart>();
        public List<Order> orders { get; set; } = new List<Order>();
        public List<Subcribe> subcribes { get; set; } = new List<Subcribe>();
    }
}
