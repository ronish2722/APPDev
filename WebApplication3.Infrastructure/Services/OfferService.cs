using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Infrastructure.Services
{
    public class OfferService:IOffer
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public OfferService(IApplicationDBContext dBContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dBContext;
            _userManager = userManager;

        }

        public async Task<Offer> CreateOfferAsync(OfferDTO offerDto)
        {
            var offerDetails = new Offer()
            {
                CreatedBy = offerDto.CreatedBy,//need to be changed according to user
                StartDate = offerDto.StartDate,
                EndDate = offerDto.EndDate,
                type = offerDto.type,
                value = offerDto.value,
                OfferDescription = offerDto.OfferDescription
            };
            await _dbContext.Offer.AddAsync(offerDetails);
            await _dbContext.SaveChangesAsync();
            return offerDetails;
        }

        public async Task<Offer> GetOffer(int id)
        {
            var offer = await _dbContext.Offer.FindAsync(id);
            return offer;
        }

        public async Task<List<OfferDTO>> GetAllOffer()
        {
            var offers = await _dbContext.Offer.ToListAsync();
            var offerDTOs = new List<OfferDTO>();
            foreach (var offer in offers)
            {
                offerDTOs.Add(new OfferDTO
                {
                    CreatedBy = offer.CreatedBy,
                    EndDate = offer.EndDate,
                    OfferDescription= offer.OfferDescription,
                    StartDate = offer.StartDate,
                    type = offer.type,
                    value = offer.value
                    // Map other properties as needed
                });
            }
            return offerDTOs;
        }

        public async Task<Offer> UpdateOfferAsync(int id, OfferDTO offerDto)
        {
            var offer = await _dbContext.Offer.FindAsync(id);
            if (offer == null)
            {
                throw new Exception("Offer not found");
            }
            offer.OfferDescription = offerDto.OfferDescription;
            offer.EndDate = offerDto.EndDate;
            offer.StartDate = offerDto.StartDate;
            offer.type = offerDto.type;
            offer.value = offerDto.value;
            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return offer;
        }

        public async Task DeleteOfferAsync(int id)
        {
            var offer = await _dbContext.Offer.FindAsync(id);
            if (offer == null)
            {
                throw new Exception("Offer not found");
            }
            _dbContext.Offer.Remove(offer);
            await _dbContext.SaveChangesAsync();
        }



    }
}
