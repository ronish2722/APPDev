using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.DTOs
{
    public class DamageFromDTO
    {
       


        public string VerifiedBy { get; set; }
        

        public DateTime Date { get; set; }

        public string description { get; set; }

        public float Amount { get; set; }

        
        public int DamageId { get; set; }

    }
}
