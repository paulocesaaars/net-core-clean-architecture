using Deviot.Administration.Domain.Exceptions;

namespace Deviot.Administration.Domain.ValueObjects.User
{
    public class FullName
    {
        public String Value { get; private set; }

        private const string INVALID_LENGTH_MIN = "O nome completo deve ter no mínimo 5 caracteres";
        private const string INVALID_LENGTH_MAX = "O nome completo deve ter no máximo 100 caracteres";

        public FullName(string value)
        {
            if (value.Length < 5)
                throw new ObjectValidationException(INVALID_LENGTH_MIN);

            if (value.Length > 100)
                throw new ObjectValidationException(INVALID_LENGTH_MAX);

            Value = value;
        }
    }
}
