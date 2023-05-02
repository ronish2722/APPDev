using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Infrastructure.Services
{
    public class PaymentService:IPayment
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PaymentService(IApplicationDBContext dBContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dBContext;
            _userManager = userManager;

        }

        public async Task<Payment> CreatePaymentAsync(PaymentDTO paymentDto)
        {
            var paymentDetails = new Payment()
            {
                UserId = paymentDto.UserId, //need to be changed according to user
                //ApprovedBy = requestDto.ApprovedBy,
                RequestsId = paymentDto.RequestsId,
                Amount = paymentDto.Amount,
                DamageId = paymentDto?.DamageId,


            };
            await _dbContext.Payment.AddAsync(paymentDetails);
            await _dbContext.SaveChangesAsync();
            return paymentDetails;
        }

        public async Task<Payment> GetPayment(int id)
        {
            var payment = await _dbContext.Payment.FindAsync(id);
            return payment;
        }

        public async Task<List<PaymentDTO>> GetAllPayment()
        {
            var payments = await _dbContext.Payment
                 //.Include(p => p.Requests)
                //.ThenInclude(r => r.Car)
                //.Include(p => p.Damage)
                .ToListAsync();
            var paymentDTOs = new List<PaymentDTO>();
            foreach (var payment in payments)
            {
                //var user = await _dbContext.Users.FindAsync(payment.UserId);
                //var carName = payment.Requests.Car.CarName;
                //var damageAmount = payment.Damage?.Amount;

                paymentDTOs.Add(new PaymentDTO
                {
                    UserId = payment.UserId, //need to be changed according to user
                    //UserName = user?.UserName,
                    RequestsId = payment.RequestsId,
                    //CarName = carName,
                    Amount = payment.Amount,

                    DamageId = payment.DamageId,
                    //DamageAmount = (float)damageAmount,
                    PaymentInfo = payment.PaymentInfo,

                    // Map other properties as needed
                }); 
            }
            return paymentDTOs;
        }

        public async Task<bool> CODPayment(int paymentId)
        {
            var payment = await _dbContext.Payment.FirstOrDefaultAsync(r => r.PaymentId == paymentId);


            payment.PaymentInfo = "Cash";
            
            _dbContext.Payment.Update(payment);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> OnlinePayment(int paymentId)
        {
            var payment = await _dbContext.Payment.FirstOrDefaultAsync(r => r.PaymentId == paymentId);


            payment.PaymentInfo = "Online";

            _dbContext.Payment.Update(payment);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<List<PaymentDTO>> GetPaymentByUser(string userId)
        {
            var payments = await _dbContext.Payment
               .Where(r => r.UserId == userId)
               .ToListAsync();

            var paymentDTOs = new List<PaymentDTO>();
            foreach (var payment in payments)
            {
                paymentDTOs.Add(new PaymentDTO
                {
                    UserId = payment.UserId, //need to be changed according to user
                    RequestsId = payment.RequestsId,
                    Amount = payment.Amount,
                    DamageId = payment.DamageId,
                    
                    // Map other properties as needed
                });
            }
            return paymentDTOs;
        }

        public async Task<Payment> UpdatePaymentAsync(int id, PaymentDTO paymentDto)
        {
            var payment = await _dbContext.Payment.FindAsync(id);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }
            payment.Amount = paymentDto.Amount;
            payment.DamageId = paymentDto.DamageId;

            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _dbContext.Payment.FindAsync(id);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }
            _dbContext.Payment.Remove(payment);
            await _dbContext.SaveChangesAsync();
        }

    }
}
