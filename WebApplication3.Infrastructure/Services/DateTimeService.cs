using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;

namespace WebApplication3.Infrastructure.Services
{
    public class DateTimeService:IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
