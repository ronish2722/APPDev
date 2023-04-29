using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class EmployeeRequestDTO
    {
        public DateTime JoinDate{get;set;}
        public string designation{get;set;}
        
        
    }
}
