using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IApplicationDBContext
    {
       
        DbSet<Address> Address { get; set; }
        DbSet<Attachment> Attachment { get; set; }
        DbSet<Car> Car { get; set; }

        DbSet<DamageNotice> Damage { get; set; }
        DbSet<Offer> Offer { get; set; }
        DbSet<Payment> Payment { get; set; }
        DbSet<Request> Request { get; set; }

        //DbSet<UserAddress> UserAddress { get; set; }
        DbSet<UserOffer> UserOffer { get; set; }

        DbSet<DamageForm> DamageForm { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
