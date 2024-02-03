using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Poco
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public bool UseAuthentication { get; set; }
        public bool UseSSl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
