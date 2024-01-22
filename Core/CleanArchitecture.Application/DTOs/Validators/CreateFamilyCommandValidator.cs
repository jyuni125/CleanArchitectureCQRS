using CleanArchitecture.Application.Commands.Family;
using CleanArchitecture.Application.DTOs.Family;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.DTOs.Validators
{
    public class CreateFamilyCommandValidator : AbstractValidator<CreateFamilyCommand>
    {
        public CreateFamilyCommandValidator()
        {
            RuleFor(data => data.FirstName)
                    .NotEmpty().WithMessage("LASTNAME IS REQUIRED!")
                    .MaximumLength(20).WithMessage("Lastname must be not exceed 20 characters");
        }
    }
}
