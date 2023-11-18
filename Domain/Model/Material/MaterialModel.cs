using System;
using Domain.Enum.OperationType;
using Domain.Model.BaseModels;

namespace Domain.Model.Payment
{
    public class MaterialModel : PagedBaseModel
    {
        public long MaterialId { get; set; }
        public long BatchId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public EnumStep Step { get; set; }
        public long CategoryId { get; set; }

        public MaterialModel() { }

        public MaterialModel(string question, string answer, EnumStep step, long categoryId, long createdByUserId) : base(createdByUserId)
        {
            Question = question;
            Answer = answer;
            Step = step;
            CategoryId = categoryId;
        }

        public new void Validate()
        {
            if (Question.Empty()) throw new ArgumentException("Question is not set");
            if (Answer.Empty()) throw new ArgumentException("Answer is not set");
            if (Step == 0) throw new ArgumentException("Step is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}