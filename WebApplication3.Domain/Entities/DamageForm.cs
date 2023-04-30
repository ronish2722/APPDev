using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Domain.Entities
{
    public class DamageForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DamageFormId { get; set; }

        [ForeignKey("StaffUser")]
        public string VerifiedBy { get; set; }
        public IdentityUser StaffUser { get; set; }

        public DateTime Date { get; set; }

        public string description { get; set; }

        public float Amount { get; set; }

        [ForeignKey("Damage")]
        public int DamageId { get; set; }

        public virtual DamageNotice Damage { get; set; }
    }
}
