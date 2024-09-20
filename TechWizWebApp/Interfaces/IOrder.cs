using TechWizWebApp.Data;

namespace WebApplication1.Interface
{
    public interface IOrder
    {
        Task<CustomResult> GetAll();

        Task<CustomResult> GetById(int id);

        Task<CustomResult> ChangeStatus(string id);

        Task<CustomResult> getByMonth(DateTime month);
    }
}
