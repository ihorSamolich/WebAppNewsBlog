using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppNewsBlog.Data.Entities
{
    [Table("Tags")]
    public class TagEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string UrlSlug { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        public virtual ICollection<PostTagMapEntity> PostTags { get; set; }
    }
}
