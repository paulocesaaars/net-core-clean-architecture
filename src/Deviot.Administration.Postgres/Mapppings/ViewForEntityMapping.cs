using AutoMapper;
using Deviot.Administration.Domain.Entities;
using Deviot.Administration.Postgres.Mapppings.Converts;
using Deviot.Administration.Postgres.Views;

namespace Deviot.Administration.Postgres.Mapppings
{
    public class ViewForEntityMapping : Profile
    {
        public ViewForEntityMapping()
        {
            AllowNullCollections = true;

            CreateMap<UserView, User>().ConvertUsing<UserViewForUserConvert>();
        }
    }
}
