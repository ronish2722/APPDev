using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IUserAddress
    {
        Task<UserAddress> AddUserAddressDetails(UserAddressDTO userAddress);

        Task<List<UserAddressDTO>> GetAllUserAddress();

        Task<List<UserAddressDTO>> GetUserAddressByUser(string userId);

        //Task<UserAddress> UpdateUserAddressAsync(int id, UserAddressDTO userAddressDto);

        //Task DeleteUserAddressAsync(int id);
    }
}
