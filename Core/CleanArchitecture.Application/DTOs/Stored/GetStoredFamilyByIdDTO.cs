﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Stored
{
    public class GetStoredFamilyByIdDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
