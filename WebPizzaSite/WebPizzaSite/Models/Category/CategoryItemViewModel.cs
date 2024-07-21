namespace WebPizzaSite.Models.Category;

/// <summary>
/// Одна категорія із БД
/// </summary>
public class CategoryItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Image { get; set; } = String.Empty;
}
