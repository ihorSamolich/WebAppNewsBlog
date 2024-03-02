using System.ComponentModel.DataAnnotations;

namespace WebAppNewsBlog.Models.Tag
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
    }
}
