using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Services.Poco;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Services
{
    public class EmailSender2 : IEmailSender2
    {
        private readonly EmailSettings _emailSettings;


        public EmailSender2(IOptions<EmailSettings> emalisettings)
        {
            _emailSettings = emalisettings.Value;
        }

        public async Task sendEmail(IEnumerable<string> emails, string subject, string body, bool isHtml = false, string? filename = null, byte[]? attachment = null, List<IFormFile> Attachments = null )
        {



            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("Panget 2 Support <no-reply@somedomain.com>");
            foreach (var e in emails)
                email.To.Add(MailboxAddress.Parse(e));
            email.Subject = subject;

            var builder = new BodyBuilder();
            if (Attachments != null)
            {
                byte[] filebytes;
                foreach (var file in Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            filebytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.Name, filebytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();


            using var smtp = new SmtpClient();
            smtp.Connect("SMTP Host",_emailSettings.Port,SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);


            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
