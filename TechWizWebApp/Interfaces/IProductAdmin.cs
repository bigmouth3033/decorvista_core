using TechWizWebApp.Domain;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Interfaces
{
    public interface IProductAdmin
    {
        public Task<CustomResult> CreateNewProduct(RequestCreateNewProduct requestCreateNewProduct);

        public Task<CustomPaging> GetProductList(int pageNumber, int pageSize);
    }
}
