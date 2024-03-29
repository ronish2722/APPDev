﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Infrastructure.Services
{
    public class AuthenticationService:IAuthentication
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        //private readonly AttachmentService _attachmentService;


        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,  IConfiguration configuration, IApplicationDBContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            
            //_attachmentService


        }

        //Registering user
        public async Task<ResponseDTO> Register(UserRegisterRequestDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            //var attachmentDTO = new AttachmentDTO();
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "User already exists!" };

            //Creating a user
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");

            //Adding address in address table of the user
            Address address = new Address()
            {
                AddressName = model.AddressName,
                Country = model.Country,
                City = model.City,
                PostalCode = (int)model.PostalCode,
            };

            _dbContext.Address.Add(address);
            await _dbContext.SaveChangesAsync();


            //Adding attachment of the user
            Domain.Entities.Attachment attachment1 = new Domain.Entities.Attachment()
            {
                DrivingLicense = model.CitizenshipOrDrivingLicense,
                //Citizenship = "string",
                NumberOfRents = 0,
                ActivityStatus = "Active",
                UserId = user.Id,
                AddressID = address.AddressId,
            };

            //file size validation
            if (model.CitizenshipOrDrivingLicense?.Length > 1500 * 1024)
            {
                return new ResponseDTO { Status = "Error", Message = "The file size cannot exceed 1.5 MB." };
            }

            _dbContext.Attachment.Add(attachment1);

            await _dbContext.SaveChangesAsync();


            if (!result.Succeeded)

                return
                        new ResponseDTO
                        { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            var attachmentDTO = new AttachmentDTO
            {
                UserId = user.Id,
                
            };

            //await _attachmentService.AddAttachmentDetails(attachmentDTO);

            return new ResponseDTO { Status = "Success", Message = "User created successfully!" };
        }

        
        //Login
        public async Task<LoginResponseDTO> Login(UserLoginRequestDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            var user = await _userManager.FindByNameAsync(model.UserName);

            //to get role of the user
            var roles = await _userManager.GetRolesAsync(user);
            var roleList = string.Join(", ", roles);

            if (result.Succeeded)

            
            {
                return new LoginResponseDTO()
                {
                    Message = $"User logged in!",
                    Status = "Success",
                    UserID = user.Id,
                    Role = roleList

                };
            }

            return new LoginResponseDTO()
            {
                Message = "User login failed! Please check user details and try again.!",
                Status = "Error"
            };

        }


        //Change Password
        public async Task<ResponseDTO> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDTO()
                {
                    Message = "User not found!",
                    Status = "Error"
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return new ResponseDTO()
                {
                    Message = "Password changed successfully!",
                    Status = "Success"
                };
            }

            return new ResponseDTO()
            {
                Message = "Failed to change password. Please check the current password and try again.",
                Status = "Error"
            };
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found!");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }


        //getuserDetails 
        public async Task<IEnumerable<UserDetailsResponse>> GetUserDetails()
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.Email,
                x.UserName,
                x.EmailConfirmed
            }).ToListAsync();

            //either
            var userDetails = from userData in users
                              select new UserDetailsResponse()
                              {
                                  UserId = userData.Id,
                                  Email = userData.Email,
                                  UserName = userData.UserName,
                                  IsEmailConfirmed = userData.EmailConfirmed
                              };

          
            

            return userDetails;
        }

        public async Task<ResponseDTO> UpdateUserDetails(string id, UserDetailsDTO model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new ResponseDTO { Status = "Error", Message = "User not found!" };
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.EmailConfirmed = model.IsEmailConfirmed;
            

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ResponseDTO { Status = "Error", Message = "Failed to update user details!" };

            }

            return new ResponseDTO { Status = "Success", Message = "User details updated successfully!" };
        }

        public async Task<ResponseDTO> ChangePasswordAsync(string userId, ChangePasswordRequestDTO model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResponseDTO
                {
                    Status = "Error",
                    Message =  "Failed to update user details!" 
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Sign the user out of all sessions after a password change
                await _signInManager.SignOutAsync();

                return new ResponseDTO
                {
                    Status = "Success",
                    Message = "User details updated successfully!"
                };
            }

            return new ResponseDTO
            {
                Status = "Error",
                Message = "Failed to update user details!"
            };
        }


        //Deleting a user
        public async Task<ResponseDTO> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new ResponseDTO { Status = "Error", Message = "User not found!" };
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return new ResponseDTO { Status = "Error", Message = "Failed to delete user!" };
            }

            return new ResponseDTO { Status = "Success", Message = "User deleted successfully!" };
        }
    }
}
