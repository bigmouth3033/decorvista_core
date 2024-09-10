using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechWizWebApp.Domain;
using TechWizWebApp.RequestModels;

namespace TechWizWebApp.Interfaces
{
    public interface IAuthAdmin
    {
        public String CreateToken(User user);

        Task<CustomResult> GetAdmin(int userId);

        Task<CustomResult> AdminLogin(String email, String password);

    }


}
