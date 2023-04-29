using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class AddCarDTO
    {
        public string CarName { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public int NumberOfRents { get; set; }
    }
}
