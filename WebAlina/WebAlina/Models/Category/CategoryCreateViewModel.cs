namespace WebAlina.Models.Category
{
    public class CategoryCreateViewModel
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
    }
}
