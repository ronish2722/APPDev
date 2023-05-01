using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomer _customerManager;

        public CustomerController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ICustomer customerManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _customerManager = customerManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDTO>>> Get()
        {
            var customers = await _customerManager.GetCustomerDetails();
            return Ok(customers);
        }

    }
}
