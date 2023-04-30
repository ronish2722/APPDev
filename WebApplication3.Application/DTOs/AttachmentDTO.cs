using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class AttachmentDTO
    {
        public string? DrivingLicense { get; set; }

        public string? Citizenship { get; set; }

        public int? NumberOfRents { get; set; }

        public string? ActivityStatus { get; set; }

        public string? UserId { get; set; }
    }
}
