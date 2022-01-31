using Deviot.Administration.Domain.Base;
using Deviot.Administration.Domain.ValueObjects.Common;
using Deviot.Administration.Domain.ValueObjects.User;

namespace Deviot.Administration.Domain.Entities
{
    public class User: EntityBase
    {
        public FullName FullName { get; protected set; }

        public Email Email { get; protected set; }

        public User(string id, string fullname, string email)
            : base(new Id(id))
        {
            FullName = new FullName(fullname);
            Email = new Email(email);
        }

        public void Setfullname(FullName value) => FullName = value;

        public void SetEmail(Email value) => Email = value;
    }
}
