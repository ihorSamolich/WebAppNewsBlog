using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Models.Category;
using WebAppNewsBlog.Models.Tag;

namespace WebAppNewsBlog.Models.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }
        public CategoryViewModel Category { get; set; }
        public ICollection<TagViewModel> Tags { get; set; }
    }
}
