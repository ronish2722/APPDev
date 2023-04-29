using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class AddressDTO
    {
        public string AddressName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }
    }
}
