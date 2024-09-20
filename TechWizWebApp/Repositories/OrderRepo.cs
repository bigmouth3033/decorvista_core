using Microsoft.EntityFrameworkCore;
using TechWizWebApp.Data;
using WebApplication1.Interface;

namespace WebApplication1.Repository
{
    public class OrderRepo : IOrder
    {
        private DecorVistaDbContext _context;
        public OrderRepo(DecorVistaDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<CustomResult> ChangeStatus(string id)
        {
            try {
                var data = await _context.Orders.SingleOrDefaultAsync(e => e.id == id);
                if (data.status == "packaged")
                {
                    data.status = "delivery";
                }
                else
                {
                    data.status = "completed";
                }
                _context.Update(data);
                await _context.SaveChangesAsync();
                return new CustomResult()
                {
                    Status = 200,
                    Message = "Change Status Success!"
                };
            }
            catch (Exception ex) {
                return new CustomResult()
                {
                    Status = 400,
                    Message = "Server Error"
                };
            }
        }

        public async Task<CustomResult> GetAll()
        {
            try {
                var list = await _context.Orders.Include(e => e.user).ThenInclude(e => e.userdetails).Select(e => new OrderRes()
                {
                    id = e.id,
                    customerName = e.user.userdetails.last_name + " " + e.user.userdetails.first_name,
                    created_date = e.created_date,
                    contact_number = e.user.userdetails.contact_number,
                    address = e.address,
                    total =e.total,
                    status = e.status
                }).ToListAsync();
                return new CustomResult()
                {
                    Status = 200,
                    data = list
                };
            }
            catch (Exception ex) {
                return new CustomResult()
                {
                    Status = 400,
                    Message = "Server Error!" + ex.InnerException.Message
                };
            }
        }

        public Task<CustomResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResult> getByMonth(DateTime month)
        {
            try
            {
                var list = await _context.Orders.Where(e => EF.Functions.DateDiffMonth(month, e.created_date) == 0).Include(e => e.user).ThenInclude(e => e.userdetails).Select(e => new OrderRes()
                {
                    id = e.id,
                    customerName = e.user.userdetails.last_name + " " + e.user.userdetails.first_name,
                    created_date = e.created_date,
                    contact_number = e.user.userdetails.contact_number,
                    address = e.user.userdetails.address,
                    total = e.total,
                    status = e.status
                }).OrderByDescending(e => e.created_date).ToListAsync();
                return new CustomResult()
                {
                    Status = 200,
                    data = list
                };
            }
            catch (Exception ex)
            {
                return new CustomResult()
                {
                    Status = 400,
                    Message = "Server Error!" + ex.InnerException.Message
                };
            }
        }

        private class OrderRes
        {
            public string id { get; set; }
            public string customerName { get; set; }
            public DateTime created_date { get; set; }

            public float total { get; set; }

            public string address { get; set; }

            public string contact_number { get; set; }

            public string status { get; set; }

        }
    }
}
