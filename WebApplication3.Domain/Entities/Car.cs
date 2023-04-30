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
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        public string Image { get; set; }

        public string CarName { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public string CarStatus { get; set; }

        public float Price { get; set; }

        public string Condition { get; set; }

        public int? NumberOfRents { get; set; }

        [ForeignKey("StaffUser")]
        public string CreatedBy { get; set; }
        public IdentityUser StaffUser { get; set; }

        public ICollection<DamageNotice> Damages { get; set; }

        public ICollection<Request> Requests { get; set; }

    }
}
