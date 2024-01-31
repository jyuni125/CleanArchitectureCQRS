using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Contracts.IServices.IServices
{
    public interface IEmailSender2
    {
        public Task sendEmail(IEnumerable<string> emails,
                 string subject,
                 string body,
                 bool isHtml = false,
                 string? filename = null,
                 byte[]? attachment = null,
                 List<IFormFile> Attachments = null);
    }
}
