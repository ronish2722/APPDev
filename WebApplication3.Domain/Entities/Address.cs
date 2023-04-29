using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Domain.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string AddressName { get; set; }

        public string Country { get; set; }  

        public string City { get; set; }

        public int PostalCode { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; } 
    }
}
