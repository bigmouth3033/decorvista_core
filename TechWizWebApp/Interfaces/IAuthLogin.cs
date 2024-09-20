using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechWizWebApp.Data;
using TechWizWebApp.Domain;


namespace WebApplication1.Interface
{
    public interface IAuthLogin
    {
        string Login(UserLogin login);
        void Register(UserDto user);
    }
    public class AuthLogin : IAuthLogin
    {
        private readonly IConfiguration _config;
        private readonly DecorVistaDbContext _context;
        public AuthLogin(IConfiguration configuration, DecorVistaDbContext decorVistaDbContext)
        {
            _config = configuration;
            _context = decorVistaDbContext;
        }

        public string Login(UserLogin login)
        {

            var user = _context.Users.Where(u => u.email == login.Email).FirstOrDefault();

            if (user == null)
            {
                return "email hoac password khong chinh xac";
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.password);
            if (isPasswordValid)
            {
                return GenerateJSONWebToken(user);
            }

            return "email hoac password khong chinh xac";
        }

        public void Register(UserDto userCreate)
        {
            var user = new User();
          
            user.email = userCreate.email;

            var userDetails = new UserDetails();
            userDetails.first_name = userCreate.userdetails.first_name;
            userDetails.last_name = userCreate.userdetails.last_name;
           

            user.userdetails = userDetails;


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreate.password);
            user.password = hashedPassword;
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.email)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public UserDetatailsDto? userdetails { get; set; }
    }
    public class UserDetatailsDto
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string contact_number { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
