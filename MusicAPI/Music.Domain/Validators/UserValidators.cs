using FluentValidation;
using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Validators
{
    public class UserValidators : AbstractValidator<User>
    {
        public UserValidators() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(30);
        }
    }
}
