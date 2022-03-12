using System;
using System.Web.Http;
using TagPortal.Core.Request.Category;
using TagPortal.Core.Service.Category;
using TbxPortal.Web.Dto.Category;

namespace TbxPortal.Web.Controllers.ClientApi.Category
{
    [RoutePrefix("api/category")]
    public class CategoryController : BaseController
    {

        private CategoryService CategoryService => Services.CategoryService;
        [Route("lookup")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri] CategoryRequest request)
        {
            var items = CategoryService.GetList(request);
            var dto = DataManagementMapper.MapList<CategoryDto>(items);

            return Ok(dto);
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPagedList([FromUri] CategoryRequest request)
        {
            if (request == null) return BadRequest();

            var pagedItems = CategoryService.GetPagedList(request);
            var dto = DataManagementMapper.MapPagedList<CategoryDto>(pagedItems);

            return Ok(dto);
        }

        [Route("{categoryId}")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] CategoryRequest request)
        {

            CategoryService.Update(request);

            return Ok();
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] CategoryRequest dto)
        {
            dto.CreatedByUserId = CurrentUser.UserId;
            dto.CreatedAt = DateTime.Now;

            CategoryService.Insert(dto);

            return Ok(new CategoryDto());
        }
    }
}