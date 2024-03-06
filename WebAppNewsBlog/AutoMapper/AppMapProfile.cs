using AutoMapper;
using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Models.Category;
using WebAppNewsBlog.Models.Post;
using WebAppNewsBlog.Models.Tag;

namespace WebAppNewsBlog.AutoMapper
{
    public class AppMapProfile : Profile
    {
        private readonly AppEFContext _context;
        public AppMapProfile(AppEFContext context)
        {
            _context = context;

            CreateMap<CategoryEntity, CategoryViewModel>();
            CreateMap<TagEntity, TagViewModel>();
            CreateMap<PostEntity, PostViewModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(pt => pt.Tag).ToList()));

            CreateMap<CreatePostViewModel, PostEntity>();
        }
    }
}