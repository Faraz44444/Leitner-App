
using System;
using System.Web.Http;
using TagPortal.Core.Request.Payment.PaymentTotal;
using TagPortal.Core.Service.Payment.PaymentTotal;
using TagPortal.Domain.Model.Payment.PaymentTotal;
using TbxPortal.Web.Dto.Payment;
using TbxPortal.Web.Dto.Payment.PaymentTotal;

namespace TbxPortal.Web.Controllers.ClientApi.Payment.PaymentTotal
{
    [RoutePrefix("api/paymenttotal")]
    public class PaymentTotalController : BaseController
    {
        private PaymentTotalService PaymentService => Services.PaymentTotalService;

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPagedList([FromUri] PaymentTotalRequest request)
        {
            if (request == null) return BadRequest();

            var pagedItems = PaymentService.GetPagedList(request);
            var dto = DataManagementMapper.MapPagedList<PaymentTotalDto>(pagedItems);

            return Ok(dto);
        }
        [Route("sum")]
        [HttpGet]
        public IHttpActionResult GetSum([FromUri] PaymentTotalRequest request)
        {
            if (request == null) return BadRequest();

            var sum = PaymentService.GetSum(request);

            return Ok(sum);
        }
        [Route("{paymentTotalId}")]
        [HttpGet]
        public IHttpActionResult GetById([FromUri] long id)
        {
            if (id < 1) return BadRequest();

            var request = new PaymentTotalRequest { PaymentTotalId = id };
            var model = PaymentService.GetById(request);
            var dto = DataManagementMapper.Map<PaymentTotalDto>(model);

            return Ok(dto);
        }

        [Route("{paymentTotalId}")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] PaymentTotalDto dto)
        {
            var model = new PaymentTotalModel()
            {
                PaymentTotalId = dto.PaymentTotalId,
                Title = dto.Title,
                Price = dto.Price,
                Date = dto.Date,
                IsDeposit = dto.IsDeposit,
            };
            PaymentService.Update(model);

            return Ok(dto);
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] PaymentTotalDto dto)
        {

            dto.CreatedByUserId = CurrentUser.UserId;
            dto.CreatedAt = DateTime.Now;
            var model = new PaymentTotalModel()
            {
                Title = dto.Title,
                Price = dto.Price,
                Date = dto.Date,
                IsDeposit = dto.IsDeposit,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt

            };
            PaymentService.Insert(model);

            return Ok(dto);
        }
    }
}

