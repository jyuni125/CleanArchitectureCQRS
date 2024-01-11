using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Models
{
    public interface IBaseModel
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
    }
}
