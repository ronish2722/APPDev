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
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int RequestId { get; set; }
        public DateTime RequestedDate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [ForeignKey("StaffUser")]
        public string? ApprovedBy { get; set; }
        public IdentityUser? StaffUser { get; set; }

        [ForeignKey("Car")]
        public int CarID { get; set; }
        public virtual Car Car { get; set; }

        public string status { get; set; }
    }
}
