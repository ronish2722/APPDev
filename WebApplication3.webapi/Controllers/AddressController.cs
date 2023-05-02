using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _addressService;

        public AddressController(IAddress addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<Address> AddCarDetails(AddressDTO address)
        {
            var data = await _addressService.AddAddressDetails(address);
            return data;
        }

    }
}
