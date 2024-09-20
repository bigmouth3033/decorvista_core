using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Cms;
using System.Numerics;
using TechWizWebApp.Data;
using TechWizWebApp.Domain;
using TechWizWebApp.Interfaces;
using TechWizWebApp.RequestModels;
using TechWizWebApp.Services;

namespace TechWizWebApp.Repositories
{
    public class DesignerAdminRepo : IDesignerAdmin
    {
        private readonly DecorVistaDbContext _context;
        private readonly IConfiguration _config;
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;

        public DesignerAdminRepo(DecorVistaDbContext context, IConfiguration config, IFileService fileService, IMailService mailService)
        {
            _context = context;
            _config = config;
            _fileService = fileService;
            _mailService = mailService;
        }

        public async Task<CustomResult> DesignerRegister(RequestDesignerRegister requestDesignerRegister)
        {
            try
            {
                var isEmailOk = await CheckEmailExist(requestDesignerRegister.Email);

                if (!isEmailOk)
                {
                    return new CustomResult(403, "Email already exist", null);
                }

                var isPhoneOk = await CheckPhoneNumberExist(requestDesignerRegister.Phone);

                if (!isPhoneOk)
                {
                    return new CustomResult(403, "Phone number already exist", null);
                }

                var newUser = new User
                {
                    email = requestDesignerRegister.Email,
                    password = BCrypt.Net.BCrypt.HashPassword(requestDesignerRegister.Password),
                };

                _context.Users.Add(newUser);

                var newDesigner = new InteriorDesigner
                {
                    address = requestDesignerRegister.Address,
                    approved_status = "pending",
                    portfolio = requestDesignerRegister.Porfolio,
                    contact_number = requestDesignerRegister.Phone,
                    user = newUser,
                    first_name = requestDesignerRegister.FirstName,
                    last_name = requestDesignerRegister.LastName,
                    specialization = requestDesignerRegister.Specialization,
                    yearsofexperience = requestDesignerRegister.Year,
                };

                if (requestDesignerRegister.Avatar != null)
                {
                    var avatar = await _fileService.UploadImageAsync(requestDesignerRegister.Avatar);
                    newDesigner.avatar = avatar;
                }

                _context.InteriorDesigners.Add(newDesigner);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Ok", newUser);

            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.email == email);

                if (user == null)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckPhoneNumberExist(string phone)
        {
            try
            {
                var user = await _context.InteriorDesigners.SingleOrDefaultAsync(u => u.contact_number == phone);

                if (user == null)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CustomResult> GetDesignerById(int id)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync( d=> d.user_id == id);

                if(designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                return new CustomResult(200, "Success", designer);
                    
            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomPaging> GetListPendingDesigner(int pageNumber, int pageSize)
        {
            try
            {
                var designers =  _context.InteriorDesigners.Include(d => d.user).Where(d => d.approved_status == "pending");

                var total = designers.Count();

                designers = designers.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


                var list = await designers.ToListAsync();

                var customPaging = new CustomPaging()
                {
                    Status = 200,
                    Message = "OK",
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)total / pageSize),
                    PageSize = pageSize,
                    TotalCount = total,
                    Data = list
                };

                return customPaging;

            }
            catch (Exception ex)
            {
                return new CustomPaging()
                {
                    Status = 400,
                    Message = ex.Message,
                    CurrentPage = pageNumber,
                    TotalPages = 0,
                    PageSize = pageSize,
                    TotalCount = 0,
                    Data = null
                };
            }
        }

        public async Task<CustomPaging> GetListApprovedDesigner(int pageNumber, int pageSize)
        {
            try
            {
                var designers = _context.InteriorDesigners.Include(d => d.user).Where(d => d.approved_status == "approved");

                var total = designers.Count();

                designers = designers.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


                var list = await designers.ToListAsync();

                var customPaging = new CustomPaging()
                {
                    Status = 200,
                    Message = "OK",
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)total / pageSize),
                    PageSize = pageSize,
                    TotalCount = total,
                    Data = list
                };

                return customPaging;

            }
            catch (Exception ex)
            {
                return new CustomPaging()
                {
                    Status = 400,
                    Message = ex.Message,
                    CurrentPage = pageNumber,
                    TotalPages = 0,
                    PageSize = pageSize,
                    TotalCount = 0,
                    Data = null
                };
            }
        }

        public async Task<CustomResult> ApproveDesigner(int designerId)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.id == designerId);

                if (designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                designer.approved_status = "approved";

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);


            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomResult> DenyDesigner(int designerId)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.id == designerId);

                if (designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                designer.approved_status = "unapproved";

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);


            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomResult> ChangeDesignerStatus(int designerId)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.id == designerId);

                if(designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                designer.status = !designer.status;

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);
            }
            catch(Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomResult> ChangeDow(int designerId, string dow)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.user_id == designerId);

                if (designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                designer.daywork = dow;

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);

            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomResult> ChangePortfolio(int designerId, string portfolio)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.user_id == designerId);

                if (designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                designer.portfolio = portfolio;

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);

            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomResult> ChangeDesignerInfo(RequestUpdateDesignerInfo requestUpdateDesignerInfo)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.id == requestUpdateDesignerInfo.Id);

                if (designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                designer.first_name = requestUpdateDesignerInfo.FirstName;
                designer.last_name = requestUpdateDesignerInfo.LastName;
                designer.contact_number = requestUpdateDesignerInfo.Phone;
                designer.address = requestUpdateDesignerInfo.Address;
                designer.specialization = requestUpdateDesignerInfo.Specialization;
                designer.yearsofexperience = requestUpdateDesignerInfo.Year;

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);

            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }

        public async Task<CustomResult> UpdateImage(int designerId, IFormFile avatar)
        {
            try
            {
                var designer = await _context.InteriorDesigners.Include(d => d.user).SingleOrDefaultAsync(d => d.user_id == designerId);

                if (designer == null)
                {
                    return new CustomResult(404, "Not found", null);
                }

                var newAvatar = await _fileService.UploadImageAsync(avatar);

                designer.avatar = newAvatar;

                _context.InteriorDesigners.Update(designer);

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", designer);

            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad request", ex.Message);
            }
        }
    }
}
