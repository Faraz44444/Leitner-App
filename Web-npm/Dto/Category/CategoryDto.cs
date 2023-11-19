namespace Web.Dto.Category
{
    public class CategoryDto : BaseDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string CreatedAtFormatted
        {
            get { return CreatedAt.Value.ToString("dd/MM/yyyy"); }
        }
    }
}
