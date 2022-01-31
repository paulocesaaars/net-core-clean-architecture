using Deviot.Administration.Domain.Exceptions;

namespace Deviot.Administration.Domain.ValueObjects.Common
{
    public class Id
    {
        public Guid Value { get; private set; }

        private const string INVALID_ID = "Id inválido";

        public Id(string value)
        {
            try
            {
                Value = new Guid(value);
            }
            catch (Exception)
            {
                throw new ObjectValidationException(INVALID_ID);
            }
        }
    }
}
