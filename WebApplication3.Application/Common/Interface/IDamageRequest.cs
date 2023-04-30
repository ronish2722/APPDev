using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IDamageRequest
    {
        Task<DamageNotice> AddDamageRequestDetails(DamageRequestDTO damageRequest);

        Task<DamageNotice> GetDamageRequest(int id);

        Task<List<DamageRequestDTO>> GetAllDamageRequest();

        Task<DamageNotice> UpdateDamageRequestAsync(int id, DamageRequestDTO damageRequestDto);

        Task DeleteDamageRequestAsync(int id);
    }
}
