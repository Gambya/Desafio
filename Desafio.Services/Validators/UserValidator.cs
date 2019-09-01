using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Services.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Null and nonexistent object");
                });

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("Missing fields")
                .NotNull().WithMessage("Missing fields");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("Missing fields")
                .NotNull().WithMessage("Missing fields");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Missing fields")
                .NotNull().WithMessage("Missing fields")
                .EmailAddress().WithMessage("Invalid fields");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Missing fields")
                .NotNull().WithMessage("Missing fields");

            RuleForEach(u => u.Phones)
                .SetValidator(new PhoneValidator());
        }


    }
}
