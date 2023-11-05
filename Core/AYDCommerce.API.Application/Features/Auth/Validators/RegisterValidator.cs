using AYDCommerce.API.Application.Features.Auth.Commands;
using AYDCommerce.API.Application.Features.Products.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Features.Auth.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithName("İsim Soyisim");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Şifre");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(x => x.Password)
                .WithName("Şifre Tekrarı");

            

        }
    }
}
