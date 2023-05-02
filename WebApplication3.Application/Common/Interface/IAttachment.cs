using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface IAttachment
    {
        Task<Attachment> AddAttachmentDetails(AttachmentDTO attachment);
        Task<List<AttachmentDTO>> GetAttachmentByUser(string userId);

        Task<Attachment> UpdateAttachmentFormAsync(int id, AttachmentDTO attachmentDto);

        Task<string> UploadFileAsync(FileUploadRequestDTO fileUploadRequestDTO);

        Task DeleteAttachmentAsync(int id);

       
    }
}
