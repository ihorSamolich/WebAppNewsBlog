using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebAppNewsBlog.Data.Entities
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string UrlSlug { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        public virtual ICollection<PostEntity> Posts { get; set; }
    }
}
