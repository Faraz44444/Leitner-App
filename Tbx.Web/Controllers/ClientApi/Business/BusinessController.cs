

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TagPortal.Core.Request.Business;
using TagPortal.Core.Request.Payment.PaymentPriority;
using TagPortal.Core.Service.Business;
using TagPortal.Core.Service.Payment.PaymentPriority;
using TagPortal.Domain.Model.Business;
using TbxPortal.Web.Dto.Business;
using TbxPortal.Web.Dto.Payment.PaymentPriority;

namespace TbxPortal.Web.Controllers.ClientApi.Business
{

    [RoutePrefix("api/business")]

    public class BusinessController : BaseController
    {
        private BusinessService PaymentPriorityService => Services.BusinessService;

        [Route("lookup")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri] BusinessRequest request)
        {
            if (request == null) request = new BusinessRequest();

            var items = PaymentPriorityService.GetList(request);
            var dto = DataManagementMapper.MapList<BusinessDto>(items);

            return Ok(dto);
        }

         [Route("")]
        [HttpGet]
        public IHttpActionResult GetPagedList([FromUri] BusinessRequest request)
        {
            if (request == null) return BadRequest();

            var pagedItems = PaymentPriorityService.GetPagedList(request);
            var dto = DataManagementMapper.MapPagedList<BusinessDto>(pagedItems);

            return Ok(dto);
        }

        [Route("{paymentId}")]
        [HttpGet]
        public IHttpActionResult GetById([FromUri] long id)
        {
            if (id < 1) return BadRequest();

            var request = new BusinessRequest { BusinessId = id };
            var model = PaymentPriorityService.GetById(id);
            var dto = DataManagementMapper.Map<BusinessDto>(model);

            return Ok(dto);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] BusinessDto dto)
        {
            dto.CreatedByUserId = CurrentUser.UserId;
            dto.CreatedAt = DateTime.Now;
            var model = new BusinessModel()
            {
                BusinessName = dto.BusinessName,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt
            };
            PaymentPriorityService.Insert(model);
            return Ok(new BusinessDto());
        }
    }
}