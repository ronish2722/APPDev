using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication3.Domain.Entities
{
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttachmentId { get; set; }

        public string? DrivingLicense { get; set; }

        public string? Citizenship { get; set; }

        public int NumberOfRents { get; set; }

        public string ActivityStatus { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }


        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }

    }
}
