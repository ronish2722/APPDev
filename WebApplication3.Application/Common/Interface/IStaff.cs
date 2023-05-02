using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Common.Interface
{
    public interface IStaff
    {
        Task<ResponseDTO> AddStaff(StaffDTO model);
        
        Task<IEnumerable<ResponseStaffDetails>> GetStaffDetails();

        Task<ResponseDTO> UpdateStaffDetails(string id, StaffDTO model);

        Task<ResponseDTO> AddAdmin(StaffDTO model);
    }
}
