using Domain.Enum.OperationType;
using System;
using Web.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public string CreatedAtFormatted
        {
            get { return CreatedAt.Value.ToString("dd/MM/yyyy"); }
        }
    }
}
