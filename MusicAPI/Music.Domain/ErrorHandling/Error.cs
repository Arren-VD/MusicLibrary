using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Music.Domain.ErrorHandling
{
    public  class Error
    {
        public Error(ErrorValues code,string keyword, string parameter)
        {
            Parameter = parameter;
            this.Code = code;
            Keyword = keyword;
            SetMessage();
        }
        public Error(ErrorValues code, string keyword, string parameter, string submittedValue)
        {
            SubmittedValue = submittedValue;
            Parameter = parameter;
            this.Code = code;
            Keyword = keyword;
            SetMessage();
        }
        public Error()
        { 

        }
        public ErrorValues Code;
        public string Message { get; set; }
        public enum ErrorValues {
        AlreadyExists = 1,
        Empty = 2,
        Invalid = 3,
        MaximumLength = 4,
        MinimumLength = 5,
        NeedsUppercase = 6,
        NeedsLowerCase = 7,
        NeedsDigit = 8,
        NeedsSymbol = 9,
        DoesNotExist = 10,
        AuthenticationError = 11
        }
        public string Keyword { get; set; }
        public string Parameter { get; set; }
        private string SubmittedValue { get; set; }

        private void SetMessage()
        {
            string baseString = $"{Keyword} {Parameter.ToLower()}";
            switch (Code)
            {
                case ErrorValues.AlreadyExists:
                    Message = SubmittedValue is null ? $"{Keyword} with submitted {Parameter.ToLower()} already exists" : $"{Keyword} with {Parameter.ToLower()} '{SubmittedValue}' already exists"; ;
                    break;
                case ErrorValues.Empty:
                    Message = $"{baseString} must not be empty";
                    break;
                case ErrorValues.Invalid:
                    Message = $"Invalid {Keyword} {Parameter.ToLower()}";
                    break;
                case ErrorValues.MaximumLength:
                    Message = $"{baseString} exceeds maximum length";
                    break;
                case ErrorValues.MinimumLength:
                    Message = $"{baseString} is below minimum length";
                    break;
                case ErrorValues.NeedsUppercase:
                    Message = $"{baseString} needs atleast one uppercase character";
                    break;
                case ErrorValues.NeedsLowerCase:
                    Message = $"{baseString} needs atleast one lowercase character";
                    break;
                case ErrorValues.NeedsDigit:
                    Message = $"{baseString} needs atleast one digit";
                    break;
                case ErrorValues.NeedsSymbol:
                    Message = $"{baseString} needs atleast one symbol";
                    break;
                case ErrorValues.DoesNotExist:
                    Message = SubmittedValue is null ?  $"{Keyword} with submited {Parameter} does not exist" : $"{Keyword} with {Parameter.ToLower()} '{SubmittedValue}' does not exist";
                    break;
                case ErrorValues.AuthenticationError:
                    Message = $"Failed to authenticate";
                    break;
                default: throw new ArgumentException();
            }
        }

    }
}
