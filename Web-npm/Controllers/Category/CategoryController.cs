using Core.Request.Business;
using Core.Request.Category;
using Core.Service;
using Domain.Enum.Category;
using Domain.Enum.Permission;
using Domain.Model.Business;
using Domain.Model.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto.Business;
using Web.Dto.Category;
using Web0.Dto;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private Service<CategoryRequest, CategoryModel> CategoryService => Services.CategoryService;
        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] CategoryRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            CategoryService.Request = req;
            var data = await CategoryService.GetList();

            return Ok(Mapper.MapList<CategoryDto>(data));
        }

        [Route("priorities")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public IActionResult GetCategoryPriorities([FromQuery] CategoryRequest req)
        {
            var result = new List<CategoryPriorityDto>();
            foreach (var i in Enum.GetValues(typeof(EnumCategoryPriority)))
            {
                var name = Enum.GetName(typeof(EnumCategoryPriority), i);
                name = string.Concat(name.Select((x, index) => Char.IsUpper(x) && index > 1 ? " " + x : x.ToString())).TrimStart(' ');
                result.Add(new CategoryPriorityDto((int)i, name));
            }
            return Ok(result);
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] CategoryRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            CategoryService.Request = req;
            var data = await CategoryService.GetPaged();
            return Ok(Mapper.MapPagedList<CategoryDto>(data));
        }

        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] CategoryDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new CategoryModel(
                name: dto.Name,
                priority: dto.Priority,
                weeklyLimit: dto.WeeklyLimit,
                monthlyLimit: dto.MonthlyLimit,
                clientId: CurrentUser.CurrentClientId,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName,
                deleted: false);
            try
            {
                CategoryService.Request = new CategoryRequest { Name = dto.Name.ToLower(), Priority = dto.Priority };
                var existingBusinesses = await CategoryService.GetList();
                if (existingBusinesses != null && existingBusinesses.Count() > 0)
                {
                    return BadRequest("There is already a Business with the given name");
                }

                CategoryService.Model = model;
                var id = await CategoryService.Insert();

                var request = new CategoryRequest()
                {
                    ClientId = CurrentUser.CurrentClientId,
                    CategoryId = id
                };
                CategoryService.Request = request;
                model = await CategoryService.GetById();
                return Ok(new DefaultResponseDto<CategoryDto>(Mapper.Map<CategoryDto>(model)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            if (id < 1) return BadRequest();
            var request = new CategoryRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId = CurrentUser.UserId,
                CategoryId = id
            };
            CategoryService.Request = request;
            var model = await CategoryService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            return Ok(Mapper.Map<CategoryDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] CategoryDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.CategoryId != id) return BadRequest();
            var request = new CategoryRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId = CurrentUser.UserId,
                CategoryId = id
            };
            CategoryService.Request = request;
            var model = await CategoryService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Name = dto.Name;
            model.Priority = dto.Priority;
            model.WeeklyLimit = dto.WeeklyLimit;
            model.MonthlyLimit = dto.MonthlyLimit;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                CategoryService.Model = model;
                await CategoryService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<CategoryDto>(Mapper.Map<CategoryDto>(await CategoryService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await CategoryService.GetById();
            if (model == null || model.CategoryId < 1 || model.CreatedByUserId != CurrentUser.UserId) return BadRequest();

            CategoryService.Request = new CategoryRequest() { CategoryId = id };
            await CategoryService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
