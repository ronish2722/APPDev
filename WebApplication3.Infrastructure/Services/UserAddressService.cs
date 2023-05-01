//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebApplication3.Application.Common.Interface;
//using WebApplication3.Application.DTOs;
//using WebApplication3.Domain.Entities;

//namespace WebApplication3.Infrastructure.Services
//{
//    public class UserAddressService:IUserAddress
//    {
//        private readonly IApplicationDBContext _dbContext;


//        public UserAddressService(IApplicationDBContext dBContext)
//        {
//            _dbContext = dBContext;

//        }

//        public async Task<UserAddress> AddUserAddressDetails(UserAddressDTO userAddress)
//        {
//            var userAddressDetails = new UserAddress()
//            {
//                UserId = userAddress.UserId,
//                AddressID = userAddress.AddressID

//            };
//            await _dbContext.UserAddress.AddAsync(userAddressDetails);
//            await _dbContext.SaveChangesAsync();
//            //return userAddressDetails;
//        }

//        public async Task<List<UserAddressDTO>> GetUserAddressByUser(string userId)
//        {
//            var userAddresses = await _dbContext.UserAddress
//               .Where(r => r.UserId == userId)
//               .ToListAsync();

//            var userAddressDTOs = new List<UserAddressDTO>();
//            foreach (var userAddress in userAddresses)
//            {
//                userAddressDTOs.Add(new UserAddressDTO
//                {
                    
//                    UserId = userAddress.UserId,
//                    AddressID=userAddress.AddressID
//                    // Map other properties as needed
//                });
//            }
//            return userAddressDTOs;
//        }

//        public async Task<List<UserAddressDTO>> GetAllUserAddress()
//        {
//            var userAddresses = await _dbContext.UserAddress.ToListAsync();
//            var userAddressDTOs = new List<UserAddressDTO>();
//            foreach (var userAddress in userAddresses)
//            {
//                userAddressDTOs.Add(new UserAddressDTO
//                {
//                    UserId=userAddress.UserId,
//                    AddressID=userAddress.AddressID,

//                    // Map other properties as needed
//                });
//            }
//            return userAddressDTOs;
//        }

//        //public async Task<UserAddress> UpdateUserAddressAsync(int id, UserAddressDTO userAddressDto)
//        //{
//        //    var userAddress = await _dbContext.UserAddress.FindAsync(id);
//        //    if (userAddress == null)
//        //    {
//        //        throw new Exception("UserAddress not found");
//        //    }
//        //    userAddress.AddressID = userAddressDto.AddressID;
            

//        //    // Update other properties as needed
//        //    await _dbContext.SaveChangesAsync();
//        //    return userAddress;
//        //}

//        //public async Task DeleteUserAddressAsync(int id)
//        //{
//        //    var userAddress = await _dbContext.UserAddress.FindAsync(id);
//        //    if (userAddress == null)
//        //    {
//        //        throw new Exception("UserAddress not found");
//        //    }
//        //    _dbContext.UserAddress.Remove(userAddress);
//        //    await _dbContext.SaveChangesAsync();
//        //}
//    }
//}
