using Desafio.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Services.Validators
{
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(p=>p)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Null and nonexistent object");
                });

            RuleFor(p => p.Number)
                .MinimumLength(8).WithMessage("Invalid fields")
                .MaximumLength(11).WithMessage("Invalid fields")
                .WithMessage("Invalid fields");

            RuleFor(p => p.AreaCode)
                .GreaterThan(2).WithMessage("Invalid fields");

            RuleFor(p => p.CountryCode)
                .MaximumLength(3).WithMessage("Invalid fields");
        }
    }
}
