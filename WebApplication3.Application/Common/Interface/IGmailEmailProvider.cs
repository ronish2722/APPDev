using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;

namespace WebApplication3.Application.Common.Interface
{
    public interface IGmailEmailProvider
    {
        //Email(IConfiguration configuration);
        Task SendEmailAsync(EmailMessage message);
    }
}
