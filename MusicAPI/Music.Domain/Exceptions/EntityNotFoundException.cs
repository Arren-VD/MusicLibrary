using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        [Obsolete]
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string parameter,Type entityType, object entityValue, Exception innerException = null)
            : base(GetErrorMessage(parameter,entityType, entityValue), innerException)
        {
        }

        private static string GetErrorMessage(string parameter,Type entityType, object entityValue)
        {
            var conditionalIdStatement = entityValue is null || (int)entityValue == 0 ? $"{entityType.Name} entity is missing a valid {parameter} " : $"{entityType.Name} with {parameter} {entityValue} could not be found.";

            return conditionalIdStatement;
        }
    }
}
