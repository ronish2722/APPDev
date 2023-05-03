using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static WebApplication3.Infrastructure.Services.Email;
using WebApplication3.Application.DTOs;
using WebApplication3.Application.Common.Interface;

namespace WebApplication3.Infrastructure.Services
{
    public class Email: IGmailEmailProvider
    {

            private readonly string _from;
             private readonly SmtpClient _client;

        //To sent email
             public Email(IConfiguration configuration)
                    {
                        var userName = configuration.GetSection("GmailCredentials:UserName").Value!;
                       var password = configuration.GetSection("GmailCredentials:Password").Value!;

                         _from = userName;
                         _client = new SmtpClient("smtp.gmail.com", 587)
                        {
                            Credentials = new NetworkCredential(userName, password),
                            UseDefaultCredentials = false,
                            EnableSsl = true
                         };
    }




        public async Task SendEmailAsync(EmailMessage message)
        {
            var mailMessage = new MailMessage(_from, message.To, message.Subject, message.Body);
            
            await _client.SendMailAsync(mailMessage);
            }
 }
}

