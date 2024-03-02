using System.ComponentModel.DataAnnotations;

namespace WebAppNewsBlog.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
    }
}
