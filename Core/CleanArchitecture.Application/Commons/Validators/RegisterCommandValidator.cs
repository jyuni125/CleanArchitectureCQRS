using CleanArchitecture.Application.Commands.Auth.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

            RuleFor(d => d.Fullname)
                    .NotEmpty()
                    .MaximumLength(50)
                    .MinimumLength(2)
                    .WithName("Input name pls");

            RuleFor(d => d.Email)
                    .NotEmpty()
                    .MaximumLength(60)
                    .EmailAddress()
                    .WithMessage("A valid email address is required.")
                    .MinimumLength(8)
                    .WithName("Input Correct Email");

            RuleFor(d => d.Password)
                    .NotEmpty()
                    .MinimumLength(6)
                    .WithName("Input Password");

            RuleFor(d => d.ConfirmPassword)
                    .NotEmpty()
                    .MinimumLength(6)
                    .Equal(p => p.Password)
                    .WithName("Password Not Matched");

        }
    }
}
