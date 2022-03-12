using AutoMapper;
using TagPortal.Domain.Aggregated.User;
using TagPortal.Domain.Model.Article;
using TagPortal.Domain.Model.Business;
using TagPortal.Domain.Model.Category;
using TagPortal.Domain.Model.Payment;
using TagPortal.Domain.Model.Payment.PaymentTotal;
using TagPortal.Domain.Model.PaymentPriority;
using TagPortal.Domain.Model.Role;
using TagPortal.Domain.Model.User;
using TbxPortal.Web.Dto;
using TbxPortal.Web.Dto.Account;
using TbxPortal.Web.Dto.Business;
using TbxPortal.Web.Dto.Category;
using TbxPortal.Web.Dto.Payment;
using TbxPortal.Web.Dto.Payment.PaymentPriority;
using TbxPortal.Web.Dto.Payment.PaymentTotal;
using TbxPortal.Web.Dto.Role;
using TbxPortal.Web.Dto.User;

namespace TbxPortal.Web.Infrastructure.MapperProfile
{
    public class DefaultMapperProfile : Profile
    {
        public DefaultMapperProfile()
        {
            //USER
            CreateMap<UserModel, AccountDto>().ReverseMap();

            CreateMap<UserModel, LookupItemDto>()
                .ForMember(x => x.Id, y => y.MapFrom(m => m.UserId))
                .ForMember(x => x.Name, y => y.MapFrom(m => m.FirstName + " " + m.LastName));
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<PaymentModel, PaymentDto>().ReverseMap();
            CreateMap<PaymentTotalModel, PaymentTotalDto>().ReverseMap();
            CreateMap<PaymentPriorityModel, PaymentPriorityDto>().ReverseMap();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
            CreateMap<BusinessModel, BusinessDto>().ReverseMap();

            CreateMap<UserSiteModel, UserSiteDto>().ReverseMap();
            CreateMap<UserSiteAccess, UserSiteAccessDto>().ReverseMap();

            CreateMap<UserSupplierModel, UserSupplierDto>().ReverseMap();
            CreateMap<UserSupplierAccess, UserSupplierAccessDto>().ReverseMap();

            //ROLE
            CreateMap<RoleModel, LookupItemDto>().ForMember(x => x.Id, y => y.MapFrom(m => m.RoleId));
            CreateMap<RoleModel, RoleDto>().ReverseMap();
            CreateMap<RolePermissionModel, RolePermissionDto>().ReverseMap();

        }
    }
}