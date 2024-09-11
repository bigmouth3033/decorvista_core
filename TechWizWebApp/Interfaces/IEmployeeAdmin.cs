using TechWizWebApp.Domain;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Interfaces
{
    public interface IEmployeeAdmin
    {
        Task<CustomResult> CreateNewEmployee(RequestCreateEmployee requestCreateEmployee);

        Task<CustomPaging> GetAllEmployee(int pageNumber, int pageSize);

        Task<CustomResult> ChangeEmployeeActive(int employeeId);

        Task<CustomResult> ChangeEmployeePermission(int employeeId, string permissionName);

    }
}
