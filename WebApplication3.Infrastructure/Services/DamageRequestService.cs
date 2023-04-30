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
    public class DamageRequestService:IDamageRequest
    {
        private readonly IApplicationDBContext _dbContext;

        public DamageRequestService(IApplicationDBContext dBContext)
        {
            _dbContext = dBContext;

        }
        public async Task<DamageNotice> AddDamageRequestDetails(DamageRequestDTO damageRequest)
        {
            var damageRequestDetails = new DamageNotice()
            {
                UserId = damageRequest.UserId,
                CarID = damageRequest.CarID,
                description = damageRequest.description,
                Date = damageRequest.Date
            };
            await _dbContext.Damage.AddAsync(damageRequestDetails);
            await _dbContext.SaveChangesAsync();
            return damageRequestDetails;
        }

        public async Task<DamageNotice> GetDamageRequest(int id)
        {
            var damageRequest = await _dbContext.Damage.FindAsync(id);
            return damageRequest;
        }

        public async Task<List<DamageRequestDTO>> GetAllDamageRequest()
        {
            var damageRequests = await _dbContext.Damage.ToListAsync();
            var damageRequestDTOs = new List<DamageRequestDTO>();
            foreach (var damageRequest in damageRequests)
            {
                damageRequestDTOs.Add(new DamageRequestDTO
                {
                    UserId = damageRequest.UserId,
                    CarID = damageRequest.CarID,
                    description = damageRequest.description,
                    Date = damageRequest.Date

                    // Map other properties as needed
                });
            }
            return damageRequestDTOs;
        }

        public async Task<DamageNotice> UpdateDamageRequestAsync(int id, DamageRequestDTO damageRequestDto)
        {
            var damageRequest = await _dbContext.Damage.FindAsync(id);
            if (damageRequest == null)
            {
                throw new Exception("Damage Request not found");
            }
            damageRequest.CarID = damageRequestDto.CarID;
            damageRequest.description = damageRequestDto.description;
            damageRequest.Date = damageRequestDto.Date;

            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return damageRequest;
        }

        public async Task DeleteDamageRequestAsync(int id)
        {
            var damageRequest = await _dbContext.Damage.FindAsync(id);
            if (damageRequest == null)
            {
                throw new Exception("Damage Request not found");
            }
            _dbContext.Damage.Remove(damageRequest);
            await _dbContext.SaveChangesAsync();
        }

    }
}
