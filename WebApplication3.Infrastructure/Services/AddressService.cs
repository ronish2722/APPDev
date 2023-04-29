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
    public class AddressService:IAddress
    {
        private readonly IApplicationDBContext _dbContext;


        public AddressService(IApplicationDBContext dBContext)
        {
            _dbContext = dBContext;

        }

        public async Task<Address> AddAddressDetails(AddressDTO address)
        {
            var addressDetails = new Address()
            {
                AddressName = address.AddressName,
                Country = address.Country,
                City = address.City,
                PostalCode = address.PostalCode,
            };
            await _dbContext.Address.AddAsync(addressDetails);
            await _dbContext.SaveChangesAsync();
            return addressDetails;
        }

    }
}
