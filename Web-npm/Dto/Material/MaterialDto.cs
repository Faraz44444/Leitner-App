using Domain.Enum.OperationType;
using Web.Dto;

namespace Web.Dto.Payment
{
    public class MaterialDto : BaseDto
    {
        public long MaterialId { get; set; }
        public long BatchId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public EnumStep Step { get; set; }
        public long CategoryId { get; set; }
        public string StepString
        {
            get
            {
                return Step.Text();
            }
        }
    }
}
