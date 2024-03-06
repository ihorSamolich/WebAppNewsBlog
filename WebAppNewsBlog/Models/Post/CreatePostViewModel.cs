namespace WebAppNewsBlog.Models.Post
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public int CategoryId { get; set; }
        public List<string> Tags { get; set; }
    }
}
