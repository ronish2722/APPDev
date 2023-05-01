using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class CustomerResponseDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int PostalCode { get; set; }

        public int NumberOfRents { get; set; }

        public string PhoneNumber { get; set; }

        public string DrivingLicenseOrCitizenship { get; set; }



    }
}
