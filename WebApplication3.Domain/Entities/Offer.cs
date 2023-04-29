using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Domain.Entities
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string type { get; set; }

        public float value { get; set; }

        public string OfferDescription { get; set; }

        [ForeignKey("StaffUser")]
        public string CreatedBy { get; set; }
        public IdentityUser StaffUser { get; set; }

        public ICollection<UserOffer> UserOffers { get; set; }
    }
}
