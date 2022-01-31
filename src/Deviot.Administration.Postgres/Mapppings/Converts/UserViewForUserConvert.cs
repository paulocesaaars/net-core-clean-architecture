using AutoMapper;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Postgres.Views;

namespace Deviot.Administration.Postgres.Mapppings.Converts
{
    internal class UserViewForUserConvert : ITypeConverter<UserView, User>
    {
        public User Convert(UserView source, User destination, ResolutionContext context)
        {
            return new User(
                source.Id.ToString(), 
                source.FullName, 
                source.Email
                );
        }
    }
}
