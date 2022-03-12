using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TagPortal.Core.Request.Payment.PaymentPriority;
using TagPortal.Core.Service.Payment.PaymentPriority;
using TbxPortal.Web.Dto.Payment.PaymentPriority;

namespace TbxPortal.Web.Controllers.ClientApi.Payment.PaymentPriority
{
    [RoutePrefix("api/paymentpriority")]

    public class PaymentPriorityController : BaseController
    {
        private PaymentPriorityService PaymentPriorityService => Services.PaymentPriorityService;

        [Route("lookup")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri] PaymentPriorityRequest request)
        {
            if (request == null) request = new PaymentPriorityRequest();

            var items = PaymentPriorityService.GetList(request);
            var dto = DataManagementMapper.MapList<PaymentPriorityDto>(items);

            return Ok(dto);
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPagedList([FromUri] PaymentPriorityRequest request)
        {
            if (request == null) return BadRequest();

            var pagedItems = PaymentPriorityService.GetPagedList(request);
            var dto = DataManagementMapper.MapPagedList<PaymentPriorityDto>(pagedItems);

            return Ok(dto);
        }

        [Route("{paymentId}")]
        [HttpGet]
        public IHttpActionResult GetById([FromUri] long id)
        {
            if (id < 1) return BadRequest();

            var request = new PaymentPriorityRequest { PaymentPriorityId = id };
            var model = PaymentPriorityService.GetById(id);
            var dto = DataManagementMapper.Map<PaymentPriorityDto>(model);

            return Ok(dto);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] PaymentPriorityRequest dto)
        {
            dto.CreatedByUserId = CurrentUser.UserId;
            dto.CreatedAt = DateTime.Now;
            PaymentPriorityService.Insert(dto);

            return Ok(new PaymentPriorityDto());
        }
    }
}