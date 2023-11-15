namespace Web.Dto.Category
{
    public class CategoryPriorityDto
    {
        public CategoryPriorityDto(int categoryPriorityId, string name)
        {
            CategoryPriorityId = categoryPriorityId;
            Name = name;
        }

        public int CategoryPriorityId { get; set; }
        public string Name { get; set; }
    }
}
