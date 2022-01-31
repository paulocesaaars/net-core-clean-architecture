using AutoMapper;
using Deviot.Administration.Postgres.Mapppings;

namespace Deviot.Administration.Test.Common.Helpers
{
    public static class AutoMapperHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(options => {
                options.AddProfile(new EntityForViewMapping());
                options.AddProfile(new ViewForEntityMapping());
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
