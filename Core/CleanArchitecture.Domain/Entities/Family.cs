using CleanArchitecture.Domain.Contracts.IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Family : BaseEntity,IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Gender { get; set; }
    }
}
