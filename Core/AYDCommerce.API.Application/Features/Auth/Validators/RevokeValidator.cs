using AYDCommerce.API.Application.Features.Auth.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYDCommerce.API.Application.Features.Auth.Validators
{
    public class RevokeValidator : AbstractValidator<RevokeRequest>
    {
        public RevokeValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
