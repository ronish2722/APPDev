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
    public class AttachmentService:IAttachment
    {
        private readonly IApplicationDBContext _dbContext;


        public AttachmentService(IApplicationDBContext dBContext)
        {
            _dbContext = dBContext;

        }
        public async Task<Attachment> AddAttachmentDetails(AttachmentDTO attachment)
        {
            var attachmentDetails = new Attachment()
            {
                DrivingLicense = attachment.DrivingLicense,
                Citizenship = attachment.Citizenship ,
                NumberOfRents = (int)attachment.NumberOfRents,
                ActivityStatus = attachment.ActivityStatus,
                UserId = attachment.UserId,
                
            };
            await _dbContext.Attachment.AddAsync(attachmentDetails);
            await _dbContext.SaveChangesAsync();
            return attachmentDetails;
        }

        public async Task<List<AttachmentDTO>> GetAttachmentByUser(string userId)
        {
            var attachments = await _dbContext.Attachment
               .Where(r => r.UserId == userId)
               .ToListAsync();

            var attachmentDTOs = new List<AttachmentDTO>();
            foreach (var attachment in attachments)
            {
                attachmentDTOs.Add(new AttachmentDTO
                {
                    DrivingLicense = attachment.DrivingLicense,
                    Citizenship = attachment.Citizenship,
                    NumberOfRents = (int)attachment.NumberOfRents,
                    ActivityStatus = attachment.ActivityStatus,
                    UserId = attachment.UserId,
                    // Map other properties as needed
                });
            }
            return attachmentDTOs;
        }


        public async Task<Attachment> UpdateAttachmentFormAsync(int id, AttachmentDTO attachmentDto)
        {
            var attachment = await _dbContext.Attachment.FindAsync(id);
            if (attachment == null)
            {
                throw new Exception("Attachment not found");
            }
            attachment.DrivingLicense = attachmentDto.DrivingLicense;
            attachment.Citizenship = attachmentDto.Citizenship;
            attachment.NumberOfRents = (int)attachmentDto.NumberOfRents;
            attachment.ActivityStatus = attachmentDto.ActivityStatus;
            
            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return attachment;
        }

        public async Task DeleteAttachmentAsync(int id)
        {
            var attachment = await _dbContext.Attachment.FindAsync(id);
            if (attachment == null)
            {
                throw new Exception("Attachment not found");
            }
            _dbContext.Attachment.Remove(attachment);
            await _dbContext.SaveChangesAsync();
        }

    }
}
