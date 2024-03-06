using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppNewsBlog.Data.Entities.Images
{
    [Table("Images")]
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Path { get; set; }
    }
}
