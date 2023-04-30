using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IDamageForm
    {
        Task<DamageForm> AddDamageFormDetails(DamageFromDTO damageForm);

        Task<DamageForm> GetDamageForm(int id);

        Task<List<DamageFromDTO>> GetAllDamageForm();

        Task<DamageForm> UpdateDamageFormAsync(int id, DamageFromDTO damageFormDto);

        Task DeleteDamageFormAsync(int id);
    }
}
