using Microsoft.AspNetCore.Mvc;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController:ControllerBase
    {

        private readonly IAttachment _attachmentService;

        public AttachmentController(IAttachment attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost]
        public async Task<Attachment> AddAttachmentDetails(AttachmentDTO attachmentDto)
        {
            var data = await _attachmentService.AddAttachmentDetails(attachmentDto);
            return data;
        }

        

        [HttpGet("GetRequestsByUser")]
        public async Task<ActionResult<List<Attachment>>> GetAttachmentByUser(string userId)
        {
            var request = await _attachmentService.GetAttachmentByUser(userId);
            return Ok(request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttachment(int id, [FromBody] AttachmentDTO attachmentDto)
        {
            if (attachmentDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedAttachment = await _attachmentService.UpdateAttachmentFormAsync(id, attachmentDto);
                return Ok(updatedAttachment);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }

            if (file.Length > 1536000)
            {
                return BadRequest("File size should not exceed 1.5 MB.");
            }

            var fileBytes = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(fileBytes, 0, fileBytes.Length);

            var fileUploadRequestDTO = new FileUploadRequestDTO
            {
                FileName = file.FileName,
                FileContent = fileBytes
            };

            var filePath = await _attachmentService.UploadFileAsync(fileUploadRequestDTO);

            return Ok(new { FilePath = filePath });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            try
            {
                await _attachmentService.DeleteAttachmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
