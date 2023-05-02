using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.DTOs
{
    public class PaymentDTO
    {
        public string UserId { get; set; }
        
        public int RequestsId { get; set; }

        public string UserName { get; set; }

        public string? PaymentInfo { get; set; }

        public string CarName { get; set; }

        public float? DamageAmount { get; set; }

        public float Amount { get; set; }


        public int? DamageId { get; set; }

    }
}
