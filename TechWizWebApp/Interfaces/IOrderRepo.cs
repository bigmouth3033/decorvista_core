using TechWizWebApp.Data;

namespace TechWizWebApp.Interfaces
{
    public interface IOrderRepo
    {
    }
    public class OrderRepo : IOrderRepo
    {
        private readonly DecorVistaDbContext _context;
        public OrderRepo(DecorVistaDbContext context)
        {
            _context = context;
        }



    }
}
