//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebApplication3.Application.Common.Interface;
//using WebApplication3.Application.DTOs;
//using WebApplication3.Domain.Entities;

//namespace WebApplication3.Infrastructure.Services
//{
//    public class EmployeeDetails : IUser
//    {
//        private readonly IApplicationDBContext _dbContext;

//        public EmployeeDetails(IApplicationDBContext dBContext) { 
//            _dbContext=dBContext;
//        }
//        public async Task<Employees> AddEmployeeDetails(EmployeeRequestDTO employee)
//        {
//            var employeeDetails = new Employees()
//            {
//                JoinDate = employee.JoinDate,
//                Designation = employee.designation
//            };
//            await _dbContext.Employee.AddAsync(employeeDetails);
//            return employeeDetails;
//        }
//    }
//}
