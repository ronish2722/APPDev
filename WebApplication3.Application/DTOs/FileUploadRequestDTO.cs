using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Application.DTOs
{
    public class FileUploadRequestDTO
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
