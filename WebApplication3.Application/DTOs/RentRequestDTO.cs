using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class RentRequestDTO
    {
        public string UserId { get; set; }
        public DateTime RequestedDate { get; set; }
        public int CarID { get; set; }

        public string? ApprovedBy { get; set; }
        public string status { get; set; }
    }
}
