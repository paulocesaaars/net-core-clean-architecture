using AutoMapper;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Api.Mapppings.Converts
{
    internal class UserViewModelForUserConvert : ITypeConverter<UserViewModel, User>
    {
        public User Convert(UserViewModel source, User destination, ResolutionContext context)
        {
            return new User(
                source.Id.ToString(),
                source.FullName,
                source.Email
                );
        }
    }
}
