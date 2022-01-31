using AutoMapper;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Postgres.Views;

namespace Deviot.Administration.Postgres.Mapppings
{
    public class EntityForViewMapping : Profile
    {
        public EntityForViewMapping()
        {
            AllowNullCollections = true;

            CreateMap<User, UserView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
        }
    }
}
