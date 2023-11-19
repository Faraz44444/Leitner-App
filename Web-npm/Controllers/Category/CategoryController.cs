using Core.Request.Batch;
using Core.Service;
using Domain.Model.Batch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto;
using Web.Dto.Category;
using Web.Infrastructure.AutoMapper;

namespace Web.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private Service<CategoryRequest, CategoryModel> CategoryService => Services.CategoryService;
        [Route("lookup")]
        [HttpGet]
        public async Task<IActionResult> Lookup([FromQuery] CategoryRequest req)
        {
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            CategoryService.Request = req;
            var data = await CategoryService.GetList();

            return Ok(Mapper.MapList<CategoryDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] CategoryRequest req)
        {
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            CategoryService.Request = req;
            var data = await CategoryService.GetPaged();
            return Ok(Mapper.MapPagedList<CategoryDto>(data));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new CategoryModel(name: dto.Name, createdByUserId: CurrentUser.UserId);
            try
            {
                CategoryService.Request = new CategoryRequest { Name = dto.Name.ToLower() };
                var existingCategories = await CategoryService.GetList();
                if (!existingCategories.Empty())
                {
                    return BadRequest("There is already a Business with the given name");
                }

                CategoryService.Model = model;
                var id = await CategoryService.Insert();

                var request = new CategoryRequest()
                {
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
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            if (id < 1) return BadRequest();
            var request = new CategoryRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                CategoryId = id
            };
            CategoryService.Request = request;
            var model = await CategoryService.GetById();

            return Ok(Mapper.Map<CategoryDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] CategoryDto dto)
        {
            if (dto == null) return BadRequest();
            var request = new CategoryRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                CategoryId = id
            };
            CategoryService.Request = request;
            var model = await CategoryService.GetById();

            model.Name = dto.Name;
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
