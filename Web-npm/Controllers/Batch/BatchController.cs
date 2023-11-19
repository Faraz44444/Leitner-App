using Core.Request.Batch;
using Core.Service;
using Domain.Model.Batch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Dto;
using Web.Dto.Category;
using Web.Infrastructure.AutoMapper;

namespace Web.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : BaseController
    {
        private Service<BatchRequest, BatchModel> BatchService => Services.BatchService;
        [Route("lookup")]
        [HttpGet]
        public async Task<IActionResult> Lookup([FromQuery] BatchRequest req)
        {
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            BatchService.Request = req;
            var data = await BatchService.GetList();

            return Ok(Mapper.MapList<BatchDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] BatchRequest req)
        {
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            BatchService.Request = req;
            var data = await BatchService.GetPaged();
            return Ok(Mapper.MapPagedList<BatchDto>(data));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BatchDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new BatchModel(batchNo: dto.BatchNo, createdByUserId: CurrentUser.UserId);
            try
            {
                BatchService.Request = new BatchRequest { BatchNo = dto.BatchNo.ToLower() };
                var existingCategories = await BatchService.GetList();
                if (!existingCategories.Empty())
                {
                    return BadRequest("There is already a Business with the given name");
                }

                BatchService.Model = model;
                var id = await BatchService.Insert();

                var request = new BatchRequest()
                {
                    BatchId = id
                };
                BatchService.Request = request;
                model = await BatchService.GetById();
                return Ok(new DefaultResponseDto<BatchDto>(Mapper.Map<BatchDto>(model)));
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
            var request = new BatchRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                BatchId = id
            };
            BatchService.Request = request;
            var model = await BatchService.GetById();

            return Ok(Mapper.Map<BatchDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] BatchDto dto)
        {
            if (dto == null) return BadRequest();
            var request = new BatchRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                BatchId = id
            };
            BatchService.Request = request;
            var model = await BatchService.GetById();

            model.BatchNo = dto.BatchNo;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                BatchService.Model = model;
                await BatchService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<BatchDto>(Mapper.Map<BatchDto>(await BatchService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await BatchService.GetById();
            if (model == null || model.BatchId < 1 || model.CreatedByUserId != CurrentUser.UserId) return BadRequest();

            BatchService.Request = new BatchRequest() { BatchId = id };
            await BatchService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
