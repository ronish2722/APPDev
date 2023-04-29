using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Domain.Entities
{
    public class UserOffer
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [ForeignKey("Offer")]
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
