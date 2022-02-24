using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Views
{
    public class UserTokenBag<T>  where T : class
    {
        public List<UserTokenDTO> UserTokens { get; set; }
        public T Value { get; set; }
    }
}
