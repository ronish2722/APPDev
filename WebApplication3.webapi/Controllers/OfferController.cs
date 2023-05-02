using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;
using WebApplication3.Infrastructure.Services;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOffer _offerService;

        public OfferController(IOffer offerService)
        {
            _offerService = offerService;
        }

        [Authorize(Policy = "StaffOrAdmin")]
        [HttpPost]
        public async Task<Offer> CreateOfferAsync(OfferDTO offerDto)
        {
            var data = await _offerService.CreateOfferAsync(offerDto);
            return data;
        }

        [HttpGet("{id}")]
        public async Task<Offer> GetOffer(int id)
        {
            var offer = await _offerService.GetOffer(id);
            if (offer == null)
            {
                return null;
            }
            return offer;
        }

        [HttpGet]
        public async Task<ActionResult<List<Offer>>> GetAllOffers()
        {
            var offers = await _offerService.GetAllOffer();
            return Ok(offers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(int id, [FromBody] OfferDTO offerDto)
        {
            if (offerDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedOffer = await _offerService.UpdateOfferAsync(id, offerDto);
                return Ok(updatedOffer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(Policy = "StaffOrAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            try
            {
                await _offerService.DeleteOfferAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}
