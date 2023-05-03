using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly string _uploadFolderPath;


        public AttachmentService(IApplicationDBContext dBContext, IConfiguration configuration)
        {
            _dbContext = dBContext;
            _uploadFolderPath = configuration.GetValue<string>("FileUploadSettings:UploadFolderPath");

        }

        //Adding attachments
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


        //Getting attachments according to user
        public async Task<List<AttachmentDTO>> GetAttachmentByUser(string userId)
        {
            var attachments = await _dbContext.Attachment
               .Where(r => r.UserId == userId)
               .ToListAsync();

            //Putting in a list
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

        //Updating attachemnt
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



        public async Task<string> UploadFileAsync(FileUploadRequestDTO fileUploadRequestDTO)
        {
            var filePath = Path.Combine(_uploadFolderPath, fileUploadRequestDTO.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(fileUploadRequestDTO.FileContent);
            }

            return filePath;
        }

        //public async Task<string> UploadAsync(IAttachment file) {
        //    var fileName = file.FileName;

        //    if (!IsAllowedFileType(Path.GetExtension(fileName)))
        //        throw new Exception("Invalid file type");

        //    if(file.Length >1 *1024 *1024)
        //        throw new Exception("FIle size exceeds the limit");

        //    return await _fileProvider.UploadFileAsync(file.FileName);
        //}
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
