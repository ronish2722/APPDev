using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IUserOffer
    {
        Task<UserOffer> AddUserOfferDetails(UserOfferDTO userAddress);

        Task<List<UserOfferDTO>> GetAllUserOffer();

        Task<List<UserOfferDTO>> GetUserOfferByUser(string userId);
    }
}
