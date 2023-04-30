using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IPayment
    {
        Task<Payment> GetPayment(int id);
        Task<Payment> CreatePaymentAsync(PaymentDTO paymentDto);

        Task<List<PaymentDTO>> GetPaymentByUser(string userId);

        Task<bool> CODPayment(int paymentId);

        Task<bool> OnlinePayment(int paymentId);
        Task<List<PaymentDTO>> GetAllPayment();
        Task<Payment> UpdatePaymentAsync(int id, PaymentDTO paymentDto);
        Task DeletePaymentAsync(int id);
    }
}
