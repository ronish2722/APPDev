using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [Required(ErrorMessage ="Password is Required")]

        public string? Password { get; set; }
    }
}
