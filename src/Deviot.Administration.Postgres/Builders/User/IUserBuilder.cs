using Deviot.Administration.Postgres.Core;
using Deviot.Administration.Postgres.Views;
using System;

namespace Deviot.Administration.Postgres.Builders
{
    public interface IUserBuilder
    {
        Request FindAllRequest(string fullname, string email, int limit, int offset);

        Request FindByIdRequest(Guid id);

        Request FindByEmailRequest(string email);

        Request TotalRegistersRequest();

        Request InsertRequest(UserView user);

        Request UpdateRequest(UserView user);
    }
}
