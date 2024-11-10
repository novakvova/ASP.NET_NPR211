namespace WebAlina.Models.Category
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
    }
}
