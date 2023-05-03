using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class UserRegisterRequestDTO
    {
        [Required(ErrorMessage ="User Name is required")]

        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]

        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]

        public string? AddressName { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int? PostalCode { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]
        

        public string? Password { get; set; }

        public string? CitizenshipOrDrivingLicense { get; set; }


    }
}
