using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Infrastructure.Persistance
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>, IApplicationDBContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<Address> Address { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Car> Car { get; set; }

        public DbSet<DamageNotice> Damage { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Request> Request { get; set; }

        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserOffer> UserOffer { get; set; }

        public DbSet<DamageForm> DamageForm { get; set; }
             
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAddress>()
            .HasKey(ua => new { ua.UserId, ua.AddressID });

            modelBuilder.Entity<UserOffer>()
            .HasKey(ua => new { ua.UserId, ua.OfferId });




                modelBuilder.Entity<Address>(entity =>
                {
                    entity.HasKey(e => e.AddressId);
                    entity.Property(e => e.AddressName).IsRequired();
                    entity.Property(e => e.Country).IsRequired();
                    entity.Property(e => e.City).IsRequired();
                    entity.Property(e => e.PostalCode).IsRequired();
                });

                modelBuilder.Entity<Attachment>(entity =>
                {
                    entity.HasKey(e => e.AttachmentId);
                    entity.Property(e => e.DrivingLicense);
                    entity.Property(e => e.Citizenship);
                    entity.Property(e => e.NumberOfRents).HasDefaultValue(0);
                    entity.Property(e => e.ActivityStatus).HasDefaultValue("Active");

                    entity.HasOne(d => d.User)
                        .WithMany()
                        .HasForeignKey(d => d.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<Car>(entity =>
                {
                    entity.HasKey(e => e.CarId); ;
                    entity.Property(e => e.Image);
                    entity.Property(e => e.CarName).IsRequired();
                    entity.Property(e => e.Brand).IsRequired();
                    entity.Property(e => e.Price).IsRequired();
                    entity.Property(e => e.Condition).IsRequired();
                    entity.Property(e => e.Description).IsRequired();
                    entity.HasOne(e => e.StaffUser)
                        .WithMany()
                        .HasForeignKey(e => e.CreatedBy)
                        .OnDelete(DeleteBehavior.Restrict);
                    entity.Property(e => e.NumberOfRents).IsRequired().HasDefaultValue(0);
                    entity.Property(e => e.CarStatus).HasDefaultValue("Available");

                });

                modelBuilder.Entity<DamageNotice>(entity =>
                {
                    entity.HasKey(e => e.DamageId);
                    //entity.Property(e => e.Amount).IsRequired();

                    entity.HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

                    //entity.HasOne(e => e.StaffUser)
                    //    .WithMany()
                    //    .HasForeignKey(e => e.VerifiedBy)
                    //    .OnDelete(DeleteBehavior.Restrict);
                    entity.Property(e => e.Date).IsRequired();
                    entity.Property(e => e.description).HasMaxLength(500);

                    entity.HasOne(e => e.Car)
                        .WithMany(c => c.Damages)
                        .HasForeignKey(e => e.CarID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<DamageForm>(entity =>
            {
                entity.HasKey(e => e.DamageFormId);
                entity.HasOne(e => e.StaffUser)
                    .WithMany()
                    .HasForeignKey(e => e.VerifiedBy)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.description).HasMaxLength(500);
                entity.HasOne(e => e.Damage)
                        .WithMany()
                        .HasForeignKey(e => e.DamageId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Amount).IsRequired();


            });

                modelBuilder.Entity<Offer>(entity =>
                {
                    entity.HasKey(e => e.OfferId);
                    entity.Property(e => e.image);
                    entity.Property(e => e.StartDate).IsRequired();
                    entity.Property(e => e.EndDate).IsRequired();
                    entity.Property(e => e.type).HasMaxLength(50).IsRequired();
                    entity.Property(e => e.value).IsRequired();
                    entity.Property(e => e.OfferDescription).HasMaxLength(500);

                    entity.HasOne(e => e.StaffUser)
                        .WithMany()
                        .HasForeignKey(e => e.CreatedBy)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasMany(e => e.UserOffers)
                        .WithOne(e => e.Offer)
                        .HasForeignKey(e => e.OfferId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<Payment>(entity =>
                {
                    entity.HasKey(e => e.PaymentId);

                    entity.Property(e => e.PaymentInfo).HasMaxLength(500);

                    entity.HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(e => e.Requests)
                        .WithMany()
                        .HasForeignKey(e => e.RequestsId)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(e => e.Damage)
                        .WithMany()
                        .HasForeignKey(e => e.DamageId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Request>(entity =>
                {
                    entity.HasKey(e => e.RequestId);

                    entity.Property(e => e.status).HasDefaultValue("Pending");
                    entity.Property(e => e.RequestedDate).IsRequired();

                    entity.HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(e => e.StaffUser)
                        .WithMany()
                        .HasForeignKey(e => e.ApprovedBy)
                        .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(e => e.Car)
                        .WithMany()
                        .HasForeignKey(e => e.CarID)
                        .OnDelete(DeleteBehavior.Cascade);
                });



                modelBuilder.Entity<UserAddress>(entity =>
                {
                    entity.HasKey(e => new { e.UserId, e.AddressID });

                    //entity.HasOne(d => d.User)
                    //    .WithOne()
                    //    .HasForeignKey(d => d.UserId)
                    //    .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(d => d.Address)
                        .WithMany(p => p.UserAddresses)
                        .HasForeignKey(d => d.AddressID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<UserOffer>(entity =>
                {
                    entity.HasKey(e => new { e.UserId, e.OfferId });

                    entity.HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(e => e.Offer)
                        .WithMany(e => e.UserOffers)
                        .HasForeignKey(e => e.OfferId)
                        .OnDelete(DeleteBehavior.Cascade);
                });

        }
    }
}
