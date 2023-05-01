using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using static Npgsql.PostgresTypes.PostgresCompositeType;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Infrastructure.Services
{
    public class CustomerService:ICustomer
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly AttachmentService _attachmentService;


        public CustomerService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IApplicationDBContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            //_attachmentService
        }

        public async Task<IEnumerable<CustomerResponseDTO>> GetCustomerDetails()
        {
            var customers = await _dbContext.Attachment
                //.Where(a => a.ActivityStatus == "Active")
                .Join(_dbContext.Users,
                    a => a.UserId,
                    u => u.Id,
                    (a, u) => new { Attachment = a, User = u })
                .Join(_dbContext.Address,
                    au => au.Attachment.AddressID,
                    addr => addr.AddressId,
                    (au, addr) => new { au.Attachment, au.User, Address = addr })
                .Select(cau => new CustomerResponseDTO
                {
                    UserId = cau.Attachment.UserId,
                    UserName = cau.User.UserName,
                    Address = cau.Address.AddressName,
                    NumberOfRents = cau.Attachment.NumberOfRents,
                    City = cau.Address.City,
                    Country = cau.Address.Country,
                    PostalCode = cau.Address.PostalCode,
                    PhoneNumber = cau.User.PhoneNumber,
                    DrivingLicenseOrCitizenship = cau.Attachment.DrivingLicense ?? cau.Attachment.Citizenship
                })
            .ToListAsync();

            return customers;
        }

    }
}
