using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music.Domain.ErrorHandling
{

    public class ValidationResult<T> where T : class
    {
        public ValidationResult()
        {
            Errors = new List<Error>();
        }
        public void AddError(Error.ErrorValues errorValue,string keyName, string parameter)
        {
            Errors.Add(new Error(errorValue, keyName, parameter));
        }
        public void AddError(Error.ErrorValues errorValue, string keyName, string parameter, string submittedValue)
        {
            Errors.Add(new Error(errorValue, keyName,parameter, submittedValue));
        }
        private T pValue { get; set; }
        public T Value {
            get { 
                if (IsErrored())
                    return null;
                else return pValue;
                }
            set {
                pValue = value;
            } 
        }
        public List<Error> Errors { get; set; }


        public bool IsErrored() => Errors.Any();

    }
}
