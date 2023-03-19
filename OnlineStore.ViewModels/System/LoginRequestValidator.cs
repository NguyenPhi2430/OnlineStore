using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.System
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest> 
    {
        public LoginRequestValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must have at least 6 characters");
        }
    }
}
