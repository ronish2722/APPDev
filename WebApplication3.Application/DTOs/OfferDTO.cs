using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class OfferDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string type { get; set; }
        public float value { get; set; }
        public string OfferDescription { get; set; }
        public string CreatedBy { get; set; }
    }
}
