using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppNewsBlog.Data.Entities
{
    [Table("Posts")]
    public class PostEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Title { get; set; }

        [StringLength(5000)]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, StringLength(5000)]
        public string Meta { get; set; }

        [Required, StringLength(255)]
        public string UrlSlug { get; set; }

        public virtual bool Published { get; set; }

        public virtual DateTime PostedOn { get; set; }

        public virtual DateTime? Modified { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual CategoryEntity Category { get; set; }

        public virtual ICollection<PostTagMapEntity> PostTags { get; set; }
    }
}
