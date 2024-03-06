using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppNewsBlog.Helpers;
using WebAppNewsBlog.Interfaces.Repository;
using WebAppNewsBlog.Models.Image;

namespace WebAppNewsBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;


        public ImageController(IMapper mapper, IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(IFormFile file)
        {
            string imageName = await ImageWorker.SaveImageAsync(file);

            return Ok(new { location = imageName });
        }
    }
}
