

using TechWizWebApp.Data;

namespace WebApplication1.Interface
{
    public interface IOrderDetails
    {
        Task<CustomResult> GetByOrderId (string orderId);
    }
}
