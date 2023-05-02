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
    public class RequestService:IRequest
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RequestService(IApplicationDBContext dBContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dBContext;
            _userManager = userManager;

        }

        public async Task<Request> CreateRequestAsync(RentRequestDTO requestDto)
        {
            var requestDetails = new Request()
            {
                UserId = requestDto.UserId, //need to be changed according to user
                //ApprovedBy = requestDto.ApprovedBy,
                CarID = requestDto.CarID,
                RequestedDate = requestDto.RequestedDate,
  
            };

            
            // Check if user is inactive
            //var lastRequestDate = await _dbContext.Request
            //    .Where(r => r.UserId == requestDto.UserId)
            //    .OrderByDescending(r => r.RequestedDate)
            //    .Select(r => r.RequestedDate)
            //    .FirstOrDefaultAsync();

            //if (lastRequestDate != default(DateTime) && (DateTime.Now - lastRequestDate).TotalDays > 90)
            //{
            //    Set user as inactive
            //    var user = await _dbContext.Attachment.FirstOrDefaultAsync(u => u.UserId == requestDto.UserId);
            //    user.ActivityStatus = "Inactive";
            //    _dbContext.Attachment.Update(user);
            //    await _dbContext.SaveChangesAsync();
            //}
            await _dbContext.Request.AddAsync(requestDetails);
            await _dbContext.SaveChangesAsync();
            return requestDetails;
        }

        public async Task<Request> GetRequest(int id)
        {
            var request = await _dbContext.Request.FindAsync(id);
            return request; 
        }

        public async Task<List<RentRequestDTO>> GetAllRequest()
        {
            var requests = await _dbContext.Request
                 .Include(r => r.User)
                .Include(r => r.StaffUser)
                .Include(r => r.Car)
                 .ToListAsync();
            var requestDTOs = new List<RentRequestDTO>();
            foreach (var request in requests)
            {
                var user = await _userManager.FindByIdAsync(request.User.Id);
                var approvedByUser = await _userManager.FindByIdAsync(request.User.Id);
                requestDTOs.Add(new RentRequestDTO
                {
                    UserId = request.User.Id, 
                    UserName = request.User.UserName,//need to be changed according to user
                    ApprovedBy = request.ApprovedBy,
                    StaffName = request.StaffUser?.UserName,
                    CarID = request.CarID,
                    CarName = request.Car.CarName,
                    RequestedDate = request.RequestedDate,
                    status =request.status,
                    // Map other properties as needed
                });
            }
            return requestDTOs;
        }

        public async Task<bool> AcceptRequest(int requestId , string approvedBy)
        {
            var request = await _dbContext.Request.FirstOrDefaultAsync(r => r.RequestId == requestId);

            var car = await _dbContext.Car.FirstOrDefaultAsync(r => r.CarId == request.CarID);


            request.status = "Accepted";
            request.ApprovedBy = approvedBy;
            _dbContext.Request.Update(request);
            await _dbContext.SaveChangesAsync();

            car.CarStatus = "Not Available";
            _dbContext.Car.Update(car);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeclineRequest(int requestId,  string approvedBy)
        {
            var request = await _dbContext.Request.FirstOrDefaultAsync(r => r.RequestId == requestId);


            request.status = "Declined";
            request.ApprovedBy = approvedBy;
            _dbContext.Request.Update(request);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CompleteRequest(int requestId, string approvedBy)
        {
            var request = await _dbContext.Request.FirstOrDefaultAsync(r => r.RequestId == requestId);
            var car = await _dbContext.Car.FirstOrDefaultAsync(r => r.CarId == request.CarID);
            var user = await _dbContext.Attachment.FirstOrDefaultAsync(r => r.UserId == request.UserId);
            var currentUser = await _userManager.FindByIdAsync(request.UserId);

            //User rented car more than 5 times this month
            DateTime today = DateTime.Today;
            int rentedCarsThisMonth = _dbContext.Request
                .Where(r => r.UserId == currentUser.Id && r.RequestedDate.Month == today.Month && r.status=="Completed")
                .Count();

            //Calculating discount
            float discountPercent = currentUser != null && await _userManager.IsInRoleAsync(currentUser, "Staff") ? 0.75f : rentedCarsThisMonth > 5 ? 0.1f : 1f; 

            float amount = request.Car.Price * discountPercent;

            //Updating requests status
            request.status = "Completed";
            request.ApprovedBy = approvedBy;
            _dbContext.Request.Update(request);
            await _dbContext.SaveChangesAsync();


            //Updating car status
            car.CarStatus = "Available";
            car.NumberOfRents = car.NumberOfRents+1;
            _dbContext.Car.Update(car);
            await _dbContext.SaveChangesAsync();

            //Adding number of rents
            user.NumberOfRents++;
            _dbContext.Attachment.Update(user);
            await _dbContext.SaveChangesAsync();
            
            //Creating payment
            Payment payment = new Payment()
            {
                UserId = request.UserId,
                RequestsId = requestId,
                PaymentInfo = "Unpaid",
                Amount = amount,

            };

            _dbContext.Payment.Add(payment);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        //public async Task<List<RentRequestDTO>> GetRequestByUser(string userId)
        //{
        //    var requests = await _dbContext.Request
        //       .Where(r => r.UserId == userId)
        //       .ToListAsync();

        //    var requestDTOs = new List<RentRequestDTO>();
        //    foreach (var request in requests)
        //    {
        //        requestDTOs.Add(new RentRequestDTO
        //        {
        //            UserId = request.UserId, //need to be changed according to user
        //            ApprovedBy = request.ApprovedBy,
        //            CarID = request.CarID,
        //            RequestedDate = request.RequestedDate,
        //            status = request.status,
        //            // Map other properties as needed
        //        });
        //    }
        //    return requestDTOs;
        //}

        //public async Task<List<RentRequestDTO>> GetRequestByUser(string userId, DateTime? fromDate, DateTime? toDate)
        //{
        //    var requests = await _dbContext.Request
        //        .Where(r => r.UserId == userId && r.RequestedDate >= fromDate && r.RequestedDate <= toDate && r.status=="Completed")
        //        .ToListAsync();

        //    var requestDTOs = new List<RentRequestDTO>();
        //    foreach (var request in requests)
        //    {
        //        requestDTOs.Add(new RentRequestDTO
        //        {
        //            UserId = request.UserId,
        //            ApprovedBy = request.ApprovedBy,
        //            CarID = request.CarID,
        //            RequestedDate = request.RequestedDate,
        //            status = request.status,
        //            // Map other properties as needed
        //        });
        //    }
        //    return requestDTOs;
        //}

        public async Task<List<RentRequestDTO>> GetRequestByUser(string userId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var requests = _dbContext.Request.Where(r => r.UserId == userId && r.status == "Completed");

            if (fromDate.HasValue)
            {
                requests = requests.Where(r => r.RequestedDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                requests = requests.Where(r => r.RequestedDate <= toDate.Value);
            }

            var requestsList = await requests.ToListAsync();

            var requestDTOs = new List<RentRequestDTO>();
            foreach (var request in requestsList)
            {
                requestDTOs.Add(new RentRequestDTO
                {
                    UserId = request.UserId,
                    ApprovedBy = request.ApprovedBy,
                    CarID = request.CarID,
                    RequestedDate = request.RequestedDate,
                    status = request.status,
                    // Map other properties as needed
                });
            }
            return requestDTOs;
        }


        public async Task<List<RentRequestDTO>> GetRequestByCar(int carId)
        {
            var requests = await _dbContext.Request
               .Where(r => r.CarID == carId)
               .ToListAsync();

            var requestDTOs = new List<RentRequestDTO>();
            foreach (var request in requests)
            {
                requestDTOs.Add(new RentRequestDTO
                {
                    UserId = request.UserId, //need to be changed according to user
                    ApprovedBy = request.ApprovedBy,
                    CarID = request.CarID,
                    RequestedDate = request.RequestedDate,
                    status = request.status,
                    // Map other properties as needed
                });
            }
            return requestDTOs;
        }


        public async Task<Request> UpdateRequestAsync(int id, RentRequestDTO requestDto)
        {
            var request = await _dbContext.Request.FindAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            request.status = requestDto.status;
            
            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return request;
        }

        public async Task DeleteRequestAsync(int id)
        {
            var request = await _dbContext.Request.FindAsync(id);
            if (request == null)
            {
                throw new Exception("Request not found");
            }
            _dbContext.Request.Remove(request);
            await _dbContext.SaveChangesAsync();
        }
    }
}
