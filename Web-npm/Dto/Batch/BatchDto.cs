namespace Web.Dto.Category
{
    public class BatchDto : BaseDto
    {
        public long BatchId { get; set; }
        public string BatchNo { get; set; }
        public string CreatedAtFormatted
        {
            get { return CreatedAt.Value.ToString("dd/MM/yyyy"); }
        }
    }
}
