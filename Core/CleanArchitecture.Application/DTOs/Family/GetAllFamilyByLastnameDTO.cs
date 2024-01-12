using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Family
{
    public class GetAllFamilyByLastnameDTO
    {
        [Required(AllowEmptyStrings =true)]
        public string Lastname { get; set; } 
    }
}
