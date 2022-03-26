
using System;
using System.Web.Http;
using TagPortal.Core.Request.Payment;
using TagPortal.Core.Service.Payment;
using TagPortal.Domain.Model.Payment;
using TbxPortal.Web.Dto.Payment;

namespace TbxPortal.Web.Controllers.ClientApi.Payment
{
    [RoutePrefix("api/payment")]
    public class PaymentController : BaseController
    {
        private PaymentService PaymentService => Services.PaymentService;

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPagedList([FromUri] PaymentRequest request)
        {
            if (request == null) return BadRequest();

            var pagedItems = PaymentService.GetPagedList(request);
            var dto = DataManagementMapper.MapPagedList<PaymentDto>(pagedItems);

            return Ok(dto);
        }
        [Route("sum")]
        [HttpGet]
        public IHttpActionResult GetSum([FromUri] PaymentRequest request)
        {
            if (request == null) return BadRequest();

            var sum = PaymentService.GetSum(request);

            return Ok(DataManagementMapper.Map<PaymentSumDto>(sum));
        }
        [Route("sums")]
        [HttpGet]
        public IHttpActionResult GetSums([FromUri] PaymentRequest request)
        {
            if (request == null) return BadRequest();

            var sums = PaymentService.GetSums(request);

            return Ok(DataManagementMapper.Map<PaymentSumListDto>(sums));
        }

        [Route("savings")]
        [HttpGet]
        public IHttpActionResult GetSaving([FromUri] PaymentRequest request)
        {
            if (request == null) return BadRequest();

            var sum = PaymentService.GetSavings(request);

            return Ok(DataManagementMapper.Map<PaymentSumListDto>(sum));
        }
        [Route("firstRecordDate")]
        [HttpGet]
        public IHttpActionResult GetFirstRecordDate()
        {
            var date = PaymentService.GetFirstRecordDate();
            var dto = DataManagementMapper.Map<RecordDateDto>(date);

            return Ok(dto);
        }
        [Route("lastRecordDate")]
        [HttpGet]
        public IHttpActionResult GetLastRecordDate()
        {
            var date = PaymentService.GetLastRecordDate();
            var dto = DataManagementMapper.Map<RecordDateDto>(date);

            return Ok(dto);
        }

        [Route("{paymentId}")]
        [HttpGet]
        public IHttpActionResult GetById([FromUri] long id)
        {
            if (id < 1) return BadRequest();

            var request = new PaymentRequest { PaymentId = id };
            var model = PaymentService.GetById(request);
            var dto = DataManagementMapper.Map<PaymentDto>(model);

            return Ok(dto);
        }

        [Route("{paymentId}")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] PaymentDto dto)
        {
            var model = new PaymentModel()
            {
                PaymentId = dto.PaymentId,
                Title = dto.Title,
                PaymentPriorityId = dto.PaymentPriorityId,
                BusinessId = dto.BusinessId,
                BusinessName = dto.BusinessName,
                IsDeposit = dto.IsDeposit,
                IsPaidToPerson = dto.IsPaidToPerson,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Date = dto.Date,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt

            };
            PaymentService.Update(model);

            return Ok(dto);
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] PaymentDto dto)
        {

            dto.CreatedByUserId = CurrentUser.UserId;
            dto.CreatedAt = DateTime.Now;
            var model = new PaymentModel()
            {
                Title = dto.Title,
                PaymentPriorityId = dto.PaymentPriorityId,
                BusinessId = dto.BusinessId,
                BusinessName = dto.BusinessName,
                IsDeposit = dto.IsDeposit,
                IsPaidToPerson = dto.IsPaidToPerson,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Date = dto.Date,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt

            };
            PaymentService.Insert(model);

            return Ok(dto);
        }
    }
}

