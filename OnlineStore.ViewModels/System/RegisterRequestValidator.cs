using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.System
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(x =>x.FirstName).NotEmpty().WithMessage("First name is required");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

            RuleFor(x => x.BirthDay).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.BirthDay).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday must be not less than 100 years from now.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.Email).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email is not valid");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must have at least 6 characters");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ComfirmPassword)
                {
                    context.AddFailure("Password must match with Confirm Password");
                }
            });
        }
    }
}
