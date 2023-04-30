using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;


namespace WebApplication3.Infrastructure.Services
{
    public class AuthenticationService:IAuthentication
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task<ResponseDTO> Register(UserRegisterRequestDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new ResponseDTO { Status = "Error", Message = "User already exists!" };

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
                
            return
                    new ResponseDTO
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." };


            return new ResponseDTO { Status = "Success", Message = "User created successfully!" };
        }

        public async Task<ResponseDTO> Login(UserLoginRequestDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (result.Succeeded)
            {
                return new ResponseDTO()
                {
                    Message = "User logged in!",
                    Status = "Success"
                };
            }

            return new ResponseDTO()
            {
                Message = "User login failed! Please check user details and try again.!",
                Status = "Error"
            };

        }
        public async Task<IEnumerable<UserDetailsDTO>> GetUserDetails()
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Email,
                x.UserName,
                x.EmailConfirmed
            }).ToListAsync();

            //either
            var userDetails = from userData in users
                              select new UserDetailsDTO()
                              {
                                  Email = userData.Email,
                                  UserName = userData.UserName,
                                  IsEmailConfirmed = userData.EmailConfirmed
                              };

            //OR
            var userDatas = new List<UserDetailsDTO>();
            foreach (var item in users)
            {
                userDatas.Add(new UserDetailsDTO()
                {
                    Email = item.Email,
                    UserName = item.UserName,
                    IsEmailConfirmed = item.EmailConfirmed
                });
            }

            return userDatas;
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





    }
}
