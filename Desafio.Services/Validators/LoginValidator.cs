using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Validators
{
    public class LoginValidator : AbstractValidator<User>
    {
        public LoginValidator()
        {
            RuleFor(u => u)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Null and nonexistent object");
                });
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Missing fields")
                .NotNull().WithMessage("Missing fields")
                .EmailAddress().WithMessage("Invalid fields");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Missing fields")
                .NotNull().WithMessage("Missing fields");
        }
    }
}
