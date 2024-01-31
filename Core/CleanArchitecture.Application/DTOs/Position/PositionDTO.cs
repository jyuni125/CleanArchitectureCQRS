using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Position
{
    public class PositionDTO
    {
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required(AllowEmptyStrings =true)]
        public string Description { get; set; }
    }
}
