using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;

namespace WebApplication3.Infrastructure.Services
{
    public class StaffService:IStaff
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly AttachmentService _attachmentService;


        public StaffService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;


        }

        //Addstaff
        public async Task<ResponseDTO> AddStaff(StaffDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            //var attachmentDTO = new AttachmentDTO();
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "Staff already exists!" };

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Staff");

            if (!result.Succeeded)

                return
                        new ResponseDTO
                        { Status = "Error", Message = "Staff creation failed! Please check user details and try again." };

            //attachmentDTO.UserId = user.Id;
            //await _attachmentService.AddAttachmentDetails(attachmentDTO);

            return new ResponseDTO { Status = "Success", Message = "Staff created successfully!" };
        }
        //AddAdmin
        public async Task<ResponseDTO> AddAdmin(StaffDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            //var attachmentDTO = new AttachmentDTO();
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "Admin already exists!" };

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)

                return
                        new ResponseDTO
                        { Status = "Error", Message = "Admin creation failed! Please check user details and try again." };

            //attachmentDTO.UserId = user.Id;
            //await _attachmentService.AddAttachmentDetails(attachmentDTO);

            return new ResponseDTO { Status = "Success", Message = "Admin created successfully!" };
        }


        //Get staff details
        public async Task<IEnumerable<ResponseStaffDetails>> GetStaffDetails()
        {
            var users = await _userManager.Users.ToListAsync();

            var staffUsers = users
                .Where(u => _userManager.GetRolesAsync(u).GetAwaiter().GetResult().Contains("Staff") || _userManager.GetRolesAsync(u).GetAwaiter().GetResult().Contains("Admin"))
                .Select(x => new
                {
                    x.Id,
                    x.Email,
                    x.UserName,
                    x.PhoneNumber
                })
                .ToList();



            var userDatas = new List<ResponseStaffDetails>();
            foreach (var item in staffUsers)
            {
                userDatas.Add(new ResponseStaffDetails()
                {
                    StaffId = item.Id,
                    Email = item.Email,
                    UserName = item.UserName,
                    PhoneNumber = item.PhoneNumber,
                });
            }

            return userDatas;
        }

        public async Task<ResponseDTO> UpdateStaffDetails(string id, StaffDTO model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new ResponseDTO { Status = "Error", Message = "Staff not found!" };
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ResponseDTO { Status = "Error", Message = "Failed to update staff details!" };

            }





            return new ResponseDTO { Status = "Success", Message = "Staff details updated successfully!" };
        }
    }
}
