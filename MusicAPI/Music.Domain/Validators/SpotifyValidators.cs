using FluentValidation;
using Music.Models.SpotifyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Validators
{
    public class SpotifyValidators : AbstractValidator<SpotifyUser>
    {
        public SpotifyValidators()
        {

        }
    }
}
