using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Family
{
    public class CreateFamilyDTO : IValidatableObject
    {
        [Required(AllowEmptyStrings = true)]
        public int status { get; set; } = 0;
        [Required(AllowEmptyStrings = true)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string LastName { get; set; }
        [Required, Range(0, 1, ErrorMessage = " (0 = Male, 1 = Female)")]
        public short Gender { get; set; }

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Gender > 1)
            {
                yield return new ValidationResult("0 to 1 only");
            }

            if (FirstName.Equals("berto", StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("Bawal ang berto");
            }
        }
        
    }
}
