using Web0.Dto;

namespace Web.Dto.Business
{
    public class BusinessDto : BaseDto
    {
        public long BusinessId { get; set; }
        public string Name { get; set; }
        public long ClientId { get; set; }
    }
}
