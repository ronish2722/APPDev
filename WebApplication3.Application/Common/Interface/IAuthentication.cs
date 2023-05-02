using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Common.Interface
{
    public interface IAuthentication
    {
        Task<ResponseDTO> Register(UserRegisterRequestDTO model);
        Task<ResponseDTO> Login(UserLoginRequestDTO model);
        Task<IEnumerable<UserDetailsDTO>> GetUserDetails();

        Task<ResponseDTO> ChangePassword(string userId, string currentPassword, string newPassword);

        Task<ResponseDTO> UpdateUserDetails(string id, UserDetailsDTO model);

        Task<ResponseDTO> ChangePasswordAsync(string userId, ChangePasswordRequestDTO model);

        Task<List<string>> GetUserRoles(string userId);

        Task<ResponseDTO> DeleteUser(string id);


    }
}
