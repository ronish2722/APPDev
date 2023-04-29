//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication3.Application.Common.Interface;
//using WebApplication3.Application.DTOs;
//using WebApplication3.Domain.Entities;

//namespace WebApplication3.webapi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUser _userDetails;

//        public UserController(IUser userDetails)
//        {
//            _userDetails = userDetails;
//        }
//        [HttpPost]
//        public async Task<Employees> AddEmployeeDetails(EmployeeRequestDTO employee)
//        {
//            var data = await _userDetails.AddEmployeeDetails(employee);
//            return data;
//        }
//    }

    

    
