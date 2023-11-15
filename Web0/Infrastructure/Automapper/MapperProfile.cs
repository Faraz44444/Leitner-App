using AutoMapper;
using Domain.Model.Business;
using Domain.Model.Category;
using Domain.Model.Payment;
using Domain.Model.Report;
using Domain.Model.Role;
using Domain.Model.User;
using Web.Dto.Business;
using Web.Dto.Category;
using Web.Dto.Payment;
using Web.Dto.Report;
using Web0.Dto.Role;
using Web0.Dto.User;

namespace Web0.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<RoleModel, RoleDto>().ReverseMap();
            CreateMap<BusinessModel, BusinessDto>().ReverseMap();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
            CreateMap<PaymentPriorityModel, PaymentPriorityDto>().ReverseMap();
            CreateMap<PaymentModel, PaymentDto>().ReverseMap();
            CreateMap<PaymentTotalModel, PaymentTotalDto>().ReverseMap();
            CreateMap<MonthlyOverviewModel, MonthlyOverviewDto>().ReverseMap();

        }
    }
}
