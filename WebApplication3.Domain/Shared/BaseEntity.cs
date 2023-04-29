using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Domain.Shared
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int Deletedby { get; set; }
        public bool IsDeleted { get; set; }


    }
}
