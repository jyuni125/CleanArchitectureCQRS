using CleanArchitecture.Domain.Contracts.IEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime AddedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int status { get; set; }
    }
}
