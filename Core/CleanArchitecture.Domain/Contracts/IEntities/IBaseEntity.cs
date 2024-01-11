using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Contracts.IEntities
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; } 
        public DateTime AddedDate { get; set; } 
        public DateTime UpdateDate { get; set; } 
        public int status { get; set; }
    }
}
