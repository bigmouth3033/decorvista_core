using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using TechWizWebApp.Data;
using TechWizWebApp.Domain;
using TechWizWebApp.Interfaces;
using TechWizWebApp.Migrations;
using TechWizWebApp.RequestModels;
using TechWizWebApp.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechWizWebApp.Repositories
{
    public class EmployeeAdminRepo : IEmployeeAdmin
    {
        private readonly TechwizDbContext _context;
        private readonly IFileService _fileService;
        private readonly IMailService _mailService;


        public EmployeeAdminRepo(TechwizDbContext context, IFileService fileService, IMailService mailService)
        {
            _context = context;
            _fileService = fileService;
            _mailService = mailService;
        }

        public async Task<CustomResult> CreateNewEmployee(RequestCreateEmployee requestCreateEmployee)
        {
            try
            {
                var isEmailOk = await CheckEmailExist(requestCreateEmployee.Email);

                if (!isEmailOk)
                {
                    return new CustomResult(403, "Email already exist", null);
                }

                var isPhoneOk = await CheckPhoneNumberExist(requestCreateEmployee.Phone);

                if (!isPhoneOk)
                {
                    return new CustomResult(403, "Phone number already exist", null);
                }

                var newEmployee = new User
                {
                    Email = requestCreateEmployee.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(requestCreateEmployee.Password),
                    PhoneNumber = requestCreateEmployee.Phone,
                    FullName = requestCreateEmployee.Name,
                    Gender = requestCreateEmployee.Gender,
                    Dob = DateTime.Parse(requestCreateEmployee.Dob),
                    Role = "Employee",
                    IsEmailConfirmed = true,
                    IsActive = true
                };

                if (requestCreateEmployee.Avatar != null)
                {
                    var imageName = await _fileService.UploadImageAsync(requestCreateEmployee.Avatar);
                    newEmployee.Avatar = imageName;
                }

                var permission = new Permission();

                newEmployee.Permission = permission;

                _context.Users.Add(newEmployee);

                _mailService.SendMailAsync(newEmployee.Email, "New employee", ReturnEmployeeMailBody(newEmployee.FullName, newEmployee.Email, requestCreateEmployee.Password));

                await _context.SaveChangesAsync();

                return new CustomResult(200, "Success", newEmployee);
            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad Request", ex.Message);
            }
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

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
                var user = await _context.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone);

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

        public async Task<CustomPaging> GetAllEmployee(int pageNumber, int pageSize)
        {
            try
            {
                var employees = _context.Users.Include(u => u.Permission).Where(u => u.Role == "Employee");

                var total = employees.Count();

                employees = employees.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


                var list = await employees.ToListAsync();

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

        public string ReturnEmployeeMailBody(string employeeName, string email, string password)
        {
            var emailBody = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Account Creation</title>
            </head>
            <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;'>

            <div style='background-color: #ffffff; max-width: 600px; margin: 20px auto; border-radius: 10px; overflow: hidden; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);'>
                <div style='background-color: #4CAF50; color: white; text-align: center; padding: 20px;'>
                    <h1 style='margin: 0;'>Welcome to AAA Site</h1>
                </div>
            
                <div style='padding: 30px; color: #333333;'>
                    <p>Dear {employeeName},</p>
                    <p>We are pleased to inform you that your employee account for the <strong>AAA site</strong> has been successfully created.</p>
                
                    <p><strong>Login Details:</strong></p>
                    <p>Email: <strong>{email}</strong></p>
                    <p>Password: <strong>{password}</strong></p>
                
                    <p>To access your account, please use the button below:</p>
                    <p>
                        <a href='http://localhost:5173/login' style='display: inline-block; padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px; font-size: 16px;'>Login to AAA Site</a>
                    </p>
                
                    <p>If you have any questions or need assistance, feel free to contact our support team.</p>
                
                    <p>Best regards,<br>AAA Site Team</p>
                </div>
            
                <div style='background-color: #f4f4f4; padding: 20px; text-align: center; font-size: 12px; color: #999999;'>
                    <p>© 2024 AAA Site. All rights reserved.</p>
                </div>
            </div>

            </body>
            </html>
            ";

            return emailBody;
        }

        public async Task<CustomResult> ChangeEmployeeActive(int employeeId)
        {
            try
            {
                var employee = await _context.Users.SingleOrDefaultAsync(u => u.Id == employeeId);

                if (employee != null)
                {

                    employee.IsActive = !employee.IsActive;
                    _context.Users.Update(employee);
                    await _context.SaveChangesAsync();

                    return new CustomResult(200, "Success", null);
                }

                return new CustomResult(404, "Not found", null);

            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad Request", ex.Message);
            }
        }

        public async Task<CustomResult> ChangeEmployeePermission(int employeeId, string permissionName)
        {
            try
            {
                var employee = await _context.Users.Include(u => u.Permission).SingleOrDefaultAsync(u => u.Id == employeeId);

                if (employee != null && employee.Permission != null)
                {
                    if (permissionName == "A")
                    {
                        employee.Permission.PermissionA = !employee.Permission.PermissionA;
                    }

                    if (permissionName == "B")
                    {
                        employee.Permission.PermissionB = !employee.Permission.PermissionB;
                    }

                    if (permissionName == "C")
                    {
                        employee.Permission.PermissionC = !employee.Permission.PermissionC;
                    }

                    if (permissionName == "A" || permissionName == "B" || permissionName == "C")
                    {
                        _context.Users.Update(employee);
                        await _context.SaveChangesAsync();
                        return new CustomResult(200, "Success", null);
                    }

                }


                return new CustomResult(404, "Not found", null);
            }
            catch (Exception ex)
            {
                return new CustomResult(400, "Bad Request", ex.Message);
            }
        }
    }
}
