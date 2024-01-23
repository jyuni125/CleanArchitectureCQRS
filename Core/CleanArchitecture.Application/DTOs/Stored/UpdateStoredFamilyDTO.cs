using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Stored
{
    public class UpdateStoredFamilyDTO
    {
        [Required]
        public Guid Id { get; set; }
      
        public string? Firstname { get; set; }
        
        public string? Lastname { get; set; }
        [Range(0, 1, ErrorMessage = "(0 = Male, 1 = Female)")]
        public short? Gender { get; set; }
        public short? status { get; set; }
    }
}
