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
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [ForeignKey("Request")]
        public int RequestsId { get; set; }

        public virtual Request Requests { get; set; }

        public string PaymentInfo { get; set; }

        public float Amount { get; set; }

        [ForeignKey("Damage")]
        public int DamageId { get; set; }

        public virtual Damage Damage { get; set; }

    }
}
