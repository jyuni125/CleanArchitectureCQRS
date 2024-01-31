using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Poco
{
    public class MailRequest
    {
        public IEnumerable<string> emails { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool isHtml { get; set; } = false;
        public string? filename { get; set; }
        public byte[]? attachment { get; set; } 
        public List<IFormFile> Attachments { get; set; } 
    }
}
