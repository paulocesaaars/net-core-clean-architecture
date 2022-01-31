using Deviot.Administration.Domain.ValueObjects.Common;

namespace Deviot.Administration.Domain.Base
{
    public abstract class EntityBase
    {
        public Id Id { get; protected set; }

        protected EntityBase(Id id)
        {
            Id = id;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
