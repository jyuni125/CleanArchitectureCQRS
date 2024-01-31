using CleanArchitecture.Domain.Contracts.IServices.IServices;
using CleanArchitecture.Services.Poco;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace CleanArchitecture.Services.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emalisettings)
        {
            _emailSettings= emalisettings.Value;
        }

        public async Task send(IEnumerable<string> emails, string subject, string body, bool isHtml = false, string? filename = null, byte[]? attachment = null)
        {
            
            using var sender = new SmtpClient(_emailSettings.Server, _emailSettings.Port);
            sender.UseDefaultCredentials = _emailSettings.UseAuthentication;
            sender.EnableSsl = _emailSettings.UseSSl;
            sender.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);


            var message = new MailMessage();
            message.Subject = subject;
            foreach (var email in emails)
                message.To.Add(new MailAddress(email));
            message.Body = body;
            message.IsBodyHtml = isHtml;
            message.From = new MailAddress("Panget Support <no-reply@somedomain.com>");        //"Application Support <no-reply@somedomain.com>"
            if(attachment!=null)
            message.Attachments.Add(new Attachment(new MemoryStream(attachment),filename));


            await sender.SendMailAsync(message);
        }




        
    }
}
