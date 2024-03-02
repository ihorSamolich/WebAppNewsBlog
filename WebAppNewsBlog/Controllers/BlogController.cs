using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppNewsBlog.Interfaces.Repository;
using WebAppNewsBlog.Models.Category;
using WebAppNewsBlog.Models.Post;
using WebAppNewsBlog.Models.Tag;

namespace WebAppNewsBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;


        public BlogController(IMapper mapper, IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryViewModel>>(_categoryRepository.GetAll());

            return Ok(categories);
        }

        [HttpGet("tags")]
        public IActionResult GetAllTags()
        {
            var tags = _mapper.Map<List<TagViewModel>>(_tagRepository.GetAll());

            return Ok(tags);
        }

        [HttpGet("posts")]
        public IActionResult GetAllPosts()
        {
            var posts = _mapper.Map<List<PostViewModel>>(_postRepository.GetAll());

            return Ok(posts);
        }

        [HttpGet("post/{slug}")]
        public IActionResult GetSinglePost(string slug)
        {
            var post = _mapper.Map<PostViewModel>(_postRepository.GetBySlug(slug));

            return Ok(post);
        }

    }
}
