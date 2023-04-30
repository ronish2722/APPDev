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
    public class UserOfferService:IUserOffer
    {
        private readonly IApplicationDBContext _dbContext;


        public UserOfferService(IApplicationDBContext dBContext)
        {
            _dbContext = dBContext;

        }

        public async Task<UserOffer> AddUserOfferDetails(UserOfferDTO userOffer)
        {
            var userOfferDetails = new UserOffer()
            {
                UserId = userOffer.UserId,
                OfferId = userOffer.OfferId

            };
            await _dbContext.UserOffer.AddAsync(userOfferDetails);
            await _dbContext.SaveChangesAsync();
            return userOfferDetails;
        }

        public async Task<List<UserOfferDTO>> GetUserOfferByUser(string userId)
        {
            var userOffers = await _dbContext.UserOffer
               .Where(r => r.UserId == userId)
               .ToListAsync();

            var userOfferDTOs = new List<UserOfferDTO>();
            foreach (var userOffer in userOffers)
            {
                userOfferDTOs.Add(new UserOfferDTO
                {

                    UserId = userOffer.UserId,
                    OfferId = userOffer.OfferId
                    // Map other properties as needed
                });
            }
            return userOfferDTOs;
        }

        public async Task<List<UserOfferDTO>> GetAllUserOffer()
        {
            var userOffers = await _dbContext.UserOffer.ToListAsync();
            var userOfferDTOs = new List<UserOfferDTO>();
            foreach (var userOffer in userOffers)
            {
                userOfferDTOs.Add(new UserOfferDTO
                {
                    UserId = userOffer.UserId,
                    OfferId = userOffer.OfferId,

                    // Map other properties as needed
                });
            }
            return userOfferDTOs;
        }
    }
}
