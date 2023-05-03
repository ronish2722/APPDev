using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class LoginResponseDTO
    {
        public string? Status { get; set; }

        public string? Message { get; set; }

        public string? Role { get; set; }

        public string? UserID { get; set; }
    }
}
