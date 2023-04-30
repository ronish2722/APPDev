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
            var requests = await _dbContext.Request.ToListAsync();
            var requestDTOs = new List<RentRequestDTO>();
            foreach (var request in requests)
            {
                requestDTOs.Add(new RentRequestDTO
                {
                    UserId = request.UserId, //need to be changed according to user
                    ApprovedBy = request.ApprovedBy,
                    CarID = request.CarID,
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


            request.status = "Accepted";
            request.ApprovedBy = approvedBy;
            _dbContext.Request.Update(request);
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


        public async Task<List<RentRequestDTO>> GetRequestByUser(string userId)
        {
            var requests = await _dbContext.Request
               .Where(r => r.UserId == userId)
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
