using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Family
{
    public class UpdateFamilyDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        public int? status { get; set; } 

        [Required(AllowEmptyStrings = true)]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? LastName { get; set; }

        [Required, Range(0, 1, ErrorMessage = "(0 = Male, 1 = Female)")]
        public short? Gender { get; set; }
    }
}
