using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IOffer
    {
        Task<Offer> GetOffer(int id);
        Task<Offer> CreateOfferAsync(OfferDTO offerDto);
        Task<List<OfferDTO>> GetAllOffer();
        Task<Offer> UpdateOfferAsync(int id, OfferDTO offerDto);

        Task DeleteOfferAsync(int id);
    }
}
