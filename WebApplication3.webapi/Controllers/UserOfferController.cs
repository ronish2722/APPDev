using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOfferController : ControllerBase
    {
        private readonly IUserOffer _userOfferService;

        public UserOfferController(IUserOffer userOfferService)
        {
            _userOfferService = userOfferService;
        }

        [HttpPost("Create")]
        public async Task<UserOffer> AddUserAddressDetails(UserOfferDTO userOffer)
        {
            var data = await _userOfferService.AddUserOfferDetails(userOffer);
            return data;
        }


        [HttpGet("GetAllUserOffer")]
        public async Task<ActionResult<List<UserOffer>>> GetAllUserOffer()
        {
            var userOffers = await _userOfferService.GetAllUserOffer();
            return Ok(userOffers);
        }

        [HttpGet("GetUSerOfferByUser")]
        public async Task<ActionResult<List<UserOffer>>> GetUserOfferByUser(string userId)
        {
            var userOffer = await _userOfferService.GetUserOfferByUser(userId);
            return Ok(userOffer);
        }
    }
}
