using AutoMapper;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Api.Mapppings.Converts
{
    internal class NewUserViewModelForUserConvert : ITypeConverter<NewUserViewModel, User>
    {
        public User Convert(NewUserViewModel source, User destination, ResolutionContext context)
        {
            return new User(
                Guid.NewGuid().ToString(),
                source.FullName,
                source.Email
                );
        }
    }
}
