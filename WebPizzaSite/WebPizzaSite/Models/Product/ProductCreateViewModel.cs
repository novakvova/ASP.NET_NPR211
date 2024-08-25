using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebPizzaSite.Models.Product
{
    public class ProductCreateViewModel
    {
        [Display(Name="Назва")]
        [Required(ErrorMessage ="Вкажіть назву продукту")]
        [StringLength(250, ErrorMessage ="Назва продукту не може перевищувати 250 символів")]
        public string Name { get; set; } = null!;
        [Display(Name="Ціна")]
        public decimal Price { get; set; }
        [Display(Name="Категорія")]
        public int CategoryId { get; set; }
        [Display(Name="Оберіть список фото продукта")]
        public List<IFormFile>? Photos { get; set; }
        //Випадаючий список із категоріями
        public SelectList? CategoryList { get; set; }

    }
}
