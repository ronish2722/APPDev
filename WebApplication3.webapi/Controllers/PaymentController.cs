using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController:ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<Payment> CreatePaymentAsync(PaymentDTO paymentDto)
        {
            var data = await _paymentService.CreatePaymentAsync(paymentDto);
            return data;
        }

        [HttpGet("{id}")]
        public async Task<Payment> GetPayment(int id)
        {
            var payment = await _paymentService.GetPayment(id);
            if (payment == null)
            {
                return null;
            }
            return payment;
        }

        [HttpGet("GetPaymentsByUser/")]
        public async Task<ActionResult<List<Payment>>> GetPaymentByUser(string userId)
        {
            var payment = await _paymentService.GetPaymentByUser(userId);
            return Ok(payment);
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayment()
        {
            var payment = await _paymentService.GetAllPayment();
            return Ok(payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentDTO paymentDto)
        {
            if (paymentDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedPayment = await _paymentService.UpdatePaymentAsync(id, paymentDto);
                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("{id}/Cash")]
        public async Task<IActionResult> CODPayment(int id)
        {
            var success = await _paymentService.CODPayment(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("{id}/online")]
        public async Task<IActionResult> OnlinePayment(int id)
        {
            var success = await _paymentService.OnlinePayment(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await _paymentService.DeletePaymentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
