using AutoMapper;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Api.Mapppings
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            AllowNullCollections = true;

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));

            CreateMap<User, NewUserViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value));
        }
    }
}
