using AutoMapper;
using Domain.Model.Category;
using Domain.Model.Payment;
using Domain.Model.User;
using Web.Dto.Payment;
using Web.Dto.User;

namespace Web.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<CategoryModel, MaterialDto>().ReverseMap();
            CreateMap<MaterialModel, MaterialDto>().ReverseMap();

        }
    }
}
