using Core.Request.Payment;
using Core.Service;
using Domain.Model.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Dto;
using Web.Dto.Payment;
using Web.Infrastructure.AutoMapper;

namespace Web.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : BaseController
    {
        private Service<MaterialRequest, MaterialModel> MaterialService => Services.MaterialService;

        [Route("lookup")]
        [HttpGet]
        public async Task<IActionResult> Lookup([FromQuery] MaterialRequest req)
        {
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            MaterialService.Request = req;
            var data = await MaterialService.GetList();

            return Ok(Mapper.MapList<MaterialDto>(data));
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MaterialRequest req)
        {
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            MaterialService.Request = req;
            var data = await MaterialService.GetPaged();
            return Ok(Mapper.MapPagedList<MaterialDto>(data));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MaterialDto dto)
        {
            if (dto == null) return BadRequest();
            dto.Step = Domain.Enum.OperationType.EnumStep.Box1_EveryDay;
            var model = new MaterialModel(dto.BatchId, dto.Question, dto.Answer, dto.Step, dto.CategoryId, CurrentUser.UserId);
            try
            {
                MaterialService.Model = model;
                var id = await MaterialService.Insert();

                var request = new MaterialRequest()
                {
                    MaterialId = id
                };
                MaterialService.Request = request;
                model = await MaterialService.GetById();
                return Ok(new DefaultResponseDto<MaterialDto>(Mapper.Map<MaterialDto>(model)));
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
            var request = new MaterialRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                MaterialId = id
            };
            MaterialService.Request = request;
            var model = await MaterialService.GetById();

            return Ok(Mapper.Map<MaterialDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] MaterialDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.MaterialId != id) return BadRequest();
            var request = new MaterialRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                MaterialId = id
            };
            MaterialService.Request = request;
            var model = await MaterialService.GetById();
            if (model == null) return BadRequest("Item not found.");
            model.BatchId = dto.BatchId;
            model.CategoryId = dto.CategoryId;
            model.Question = dto.Question;
            model.Answer = dto.Answer;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;

            try
            {
                MaterialService.Model = model;
                await MaterialService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<MaterialDto>(Mapper.Map<MaterialDto>(await MaterialService.GetById())));
        }

        [Route("increaseStep/{id}")]
        [HttpPut]
        public async Task<IActionResult> IncreaseStep([FromRoute] long id, [FromBody] MaterialDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.MaterialId != id) return BadRequest();
            var request = new MaterialRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                MaterialId = id
            };
            MaterialService.Request = request;
            var model = await MaterialService.GetById();
            if (model == null) return BadRequest("Item not found.");
            model.Step += 1;
            MaterialService.Model = model;
            try
            {
                MaterialService.Model = model;
                await MaterialService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<MaterialDto>(Mapper.Map<MaterialDto>(await MaterialService.GetById())));
        }

        [Route("decreaseStep/{id}")]
        [HttpPut]
        public async Task<IActionResult> DecreaseStep([FromRoute] long id, [FromBody] MaterialDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.MaterialId != id) return BadRequest();
            var request = new MaterialRequest()
            {
                CreatedByUserId = CurrentUser.UserId,
                MaterialId = id
            };
            MaterialService.Request = request;
            var model = await MaterialService.GetById();
            if (model == null) return BadRequest("Item not found.");
            model.Step = Domain.Enum.OperationType.EnumStep.Box1_EveryDay;
            MaterialService.Model = model;

            try
            {
                MaterialService.Model = model;
                await MaterialService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<MaterialDto>(Mapper.Map<MaterialDto>(await MaterialService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await MaterialService.GetById();
            if (model == null || model.MaterialId < 1 || model.CreatedByUserId != CurrentUser.UserId) return BadRequest();

            MaterialService.Request = new MaterialRequest() { MaterialId = id };
            await MaterialService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
