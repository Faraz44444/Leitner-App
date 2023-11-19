using AutoMapper;
using Domain.Model.Batch;
using Domain.Model.Payment;
using Domain.Model.User;
using Web.Dto.Category;
using Web.Dto.Payment;
using Web.Dto.User;

namespace Web.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
            CreateMap<BatchModel, BatchDto>().ReverseMap();
            CreateMap<MaterialModel, MaterialDto>().ReverseMap();

        }
    }
}
