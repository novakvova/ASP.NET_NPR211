using Microsoft.AspNetCore.Mvc;

namespace WebAlina.Models.Product
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        /// <summary>
        /// Фото, які приходять до продукту
        /// </summary>
        [BindProperty(Name = "images[]")]
        public List<IFormFile>? Images { get; set; }
    }
}
