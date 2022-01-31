using AutoMapper;
using Deviot.Administration.Api.Mapppings.Converts;
using Deviot.Administration.Api.ViewModel;
using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Api.Mapppings
{
    public class ViewModelToEntityMapping : Profile
    {
        public ViewModelToEntityMapping()
        {
            AllowNullCollections = true;

            CreateMap<UserViewModel, User>().ConvertUsing<UserViewModelForUserConvert>();
            CreateMap<NewUserViewModel, User>().ConvertUsing<NewUserViewModelForUserConvert>();
        }
    }
}
