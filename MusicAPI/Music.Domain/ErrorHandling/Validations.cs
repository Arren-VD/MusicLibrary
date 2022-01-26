using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.ErrorHandling
{
    public abstract class Validations<T> where T : class
    {
        public List<Error> Errors;
        public Validations()
        {
            Errors = new List<Error>();
        }
        public virtual List<Error> Validate(T input)
        {
            return Errors;
        }
        public  void ValidateMaxLength( string keyWord, string parameterName, string input, int maxLength)
        {
            if (input?.Length > maxLength)
                Errors.Add(new Error(Error.ErrorValues.MaximumLength,  keyWord, parameterName));
        }
        public  void ValidateMinimumLength(string keyWord, string parameterName,string input, int minimumLength)
        {
            if (input?.Length < minimumLength)
                Errors.Add(new Error(Error.ErrorValues.MinimumLength, keyWord,  parameterName));
        }
        public void ValidateNotEmpty(string keyWord, string parameterName, string input, int minimumLength)
        {
            if (String.IsNullOrEmpty(input))
                Errors.Add(new Error(Error.ErrorValues.Empty, keyWord, parameterName));
        }
    }
}
