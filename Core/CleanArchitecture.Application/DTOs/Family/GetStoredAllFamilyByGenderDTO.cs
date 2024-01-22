using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Family
{
    public class GetStoredAllFamilyByGenderDTO
    {
        [Required]
        public string gender { get; set; }
    }
}
