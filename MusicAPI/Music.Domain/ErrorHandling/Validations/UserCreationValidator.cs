using Music.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Domain.ErrorHandling;
using Music.Views;

namespace Music.Domain.ErrorHandling.Validations
{
    public  class UserCreationValidator : Validations<UserCreationDTO>
    {
        public UserCreationValidator()
        {

        }
        
         public  override List<Error>  Validate(UserCreationDTO user)
        {
            ValidateNotEmpty("User", "Name", user.Name, 2);
            ValidateMaxLength("User","Name",user.Name, 30);
            ValidateMinimumLength("User","Name", user.Name, 2);

            return base.Validate(user);
        }
    }
}
