using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IRequest
    {
        Task<Request> GetRequest(int id);
        Task<Request> CreateRequestAsync(RentRequestDTO requestDto);
        Task<List<RentRequestDTO>> GetAllRequest();
        Task<Request> UpdateRequestAsync(int id, RentRequestDTO requestDto);
        Task DeleteRequestAsync(int id);
    }
}
