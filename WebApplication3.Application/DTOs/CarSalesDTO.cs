using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class CarSalesDTO
    {
        public int CarId { get; set; }

        public string CarName { get; set; }

        public string CarBrand
        {
            get; set;
        }

        public float CarPrice { get; set; }

        public string CarCondition { get; set; }

        public List<string> Customers { get; set; }

        public float TotalSales { get; set; }



    }
}
