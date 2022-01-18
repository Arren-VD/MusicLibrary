using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Exceptions
{
    [Serializable]
    public class EntityAlreadyExistsException : Exception
    {
        [Obsolete]
        public EntityAlreadyExistsException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public EntityAlreadyExistsException(string parameter,Type entityType, object entityValue, Exception innerException = null)
           : base(GetErrorMessage(entityType, parameter, entityValue), innerException)
        {
        }
        private static string GetErrorMessage(Type entityType, string parameter, object entityValue)
            => $"A {entityType.Name} with {parameter} {entityValue} already exists.";

    }
}
