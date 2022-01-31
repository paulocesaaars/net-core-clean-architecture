using Deviot.Administration.Domain.Exceptions;
using Deviot.Common;
using System.Text.RegularExpressions;

namespace Deviot.Administration.Domain.ValueObjects.User
{
    public class Email
    {
        public String Value { get; private set; }

        private const string EMAIL_REQUIRED = "O email é obrigatório";
        private const string INVALID_FORMAT = "O email é inválido (user@email.com)";

        public Email(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ObjectValidationException(EMAIL_REQUIRED);

            if(!Utils.ValidateEmail(value))
                throw new ObjectValidationException(INVALID_FORMAT);

            Value = value;
        }
    }
}
