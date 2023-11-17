using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Web.Dto;
using Web.Infrastructure.AutoMapper;

namespace Web.Infrastructure.AutoMapper
{
    public class MapperConfiguration
    {
        public static IMapper Mapper { get; set; }

        public static void InitAutoMapper()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile(new MapperProfile());

            var config = new global::AutoMapper.MapperConfiguration(cfg);
            Mapper = config.CreateMapper();
        }
    }

    public static class Mapper
    {
        public static Destination Map<Destination>(object data)
        {
            return MapperConfiguration.Mapper.Map<Destination>(data);
        }

        public static List<Destination> MapList<Destination>(object data)
        {
            return MapperConfiguration.Mapper.Map<List<Destination>>(data);
        }

        public static PagedDto<Destination> MapPagedList<Destination>(dynamic data)
        {
            return new PagedDto<Destination>()
            {
                Items = MapList<Destination>(data.Items),
                CurrentPage = data.CurrentPage,
                TotalNumberOfItems = data.TotalNumberOfItems,
                ItemsPerPage = data.ItemsPerPage,
                TotalPages = data.TotalPages
            };
        }

    }
}
