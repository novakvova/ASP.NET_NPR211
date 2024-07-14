using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPizzaSite.Data.Entities
{
    [Table("tbl_categories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; } = null!;

        [Required, StringLength(255)]
        public string Image { get; set; } = null!;

        [StringLength(4000)]
        public string Description { get; set; } = String.Empty;
    }
}
