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
    public class DamageFormService:IDamageForm
    {
        private readonly IApplicationDBContext _dbContext;

        public DamageFormService(IApplicationDBContext dBContext)
        {
            _dbContext = dBContext;

        }
        
        public async Task<DamageForm> AddDamageFormDetails(DamageFromDTO damageForm)
        {
            var damageFormDetails = new DamageForm()
            {
                VerifiedBy = damageForm.VerifiedBy,
                Amount = damageForm.Amount,
                description = damageForm.description,
                Date = damageForm.Date,
                DamageId = damageForm.DamageId
                
            };
            await _dbContext.DamageForm.AddAsync(damageFormDetails);
            await _dbContext.SaveChangesAsync();
            return damageFormDetails;
        }

        public async Task<DamageForm> GetDamageForm(int id)
        {
            var damageForm = await _dbContext.DamageForm.FindAsync(id);
            return damageForm;
        }

        public async Task<List<DamageFromDTO>> GetAllDamageForm()
        {
            var damageForms = await _dbContext.DamageForm.ToListAsync();
            var damageFormDTOs = new List<DamageFromDTO>();
            foreach (var damageForm in damageForms)
            {
                damageFormDTOs.Add(new DamageFromDTO
                {
                    VerifiedBy = damageForm.VerifiedBy,
                    Amount = damageForm.Amount,
                    description = damageForm.description,
                    Date = damageForm.Date,
                    DamageId = damageForm.DamageId

                    // Map other properties as needed
                });
            }
            return damageFormDTOs;
        }

        public async Task<DamageForm> UpdateDamageFormAsync(int id, DamageFromDTO damageFormDto)
        {
            var damageForm = await _dbContext.DamageForm.FindAsync(id);
            if (damageForm == null)
            {
                throw new Exception("Damage Form not found");
            }


            damageForm.Amount = damageFormDto.Amount;
            damageForm.description = damageFormDto.description;

            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return damageForm;
        }

        public async Task DeleteDamageFormAsync(int id)
        {
            var damageForm = await _dbContext.DamageForm.FindAsync(id);
            if (damageForm == null)
            {
                throw new Exception("Damage Form not found");
            }
            _dbContext.DamageForm.Remove(damageForm);
            await _dbContext.SaveChangesAsync();
        }

    }
}
