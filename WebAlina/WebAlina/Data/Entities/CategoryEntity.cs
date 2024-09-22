using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAlina.Data.Entities
{
    [Table("tbl_categories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(250)]
        public string Name { get; set; } = String.Empty;
        [StringLength(4000)]
        public string? Description { get; set; }
        [StringLength(200)]
        public string? Image { get; set; }
    }
}
