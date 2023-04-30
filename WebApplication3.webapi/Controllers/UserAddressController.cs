using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;
using WebApplication3.Infrastructure.Services;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController:ControllerBase
    {
        private readonly IUserAddress _userAddressService;

        public UserAddressController( IUserAddress userAddressService)
        {
            _userAddressService = userAddressService;
        }

        [HttpPost("Create")]
        public async Task<UserAddress> AddUserAddressDetails(UserAddressDTO userAddress)
        {
            var data = await _userAddressService.AddUserAddressDetails(userAddress);
            return data;
        }


        [HttpGet("GetAllUserAddress")]
        public async Task<ActionResult<List<UserAddress>>> GetAllUserAddress()
        {
            var userAddresses = await _userAddressService.GetAllUserAddress();
            return Ok(userAddresses);
        }

        [HttpGet("GetUSerAddressByUser")]
        public async Task<ActionResult<List<UserAddress>>> GetUserAddressByUser(string userId)
        {
            var userAddress = await _userAddressService.GetUserAddressByUser(userId);
            return Ok(userAddress);
        }

      
    }
}
