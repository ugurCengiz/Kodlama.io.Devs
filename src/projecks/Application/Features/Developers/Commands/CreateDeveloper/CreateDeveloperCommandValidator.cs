using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Developers.Commands.CreateDeveloper
{
    public class CreateDeveloperCommandValidator : AbstractValidator<CreateDeveloperCommand>
    {
        public CreateDeveloperCommandValidator()
        {
            RuleFor(d => d.Email).NotEmpty().EmailAddress();
            RuleFor(d => d.Password).NotEmpty().MinimumLength(9);
            RuleFor(d => d.FirstName).NotEmpty();
            RuleFor(d => d.LastName).NotEmpty();
        }
    }
}
