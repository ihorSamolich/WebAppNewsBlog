using Microsoft.EntityFrameworkCore;
using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Interfaces.Repository;

namespace WebAppNewsBlog.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppEFContext _context;

        public PostRepository(AppEFContext context)
        {
            _context = context;
        }

        public PostEntity Add(PostEntity entity)
        {
            _context.Posts.Add(entity);
            return entity;
        }

        public int Count()
        {
            return _context.Posts.Count();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PostEntity> GetAll()
        {
            return
                _context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.PostTags)
                        .ThenInclude(t => t.Tag)
                    .OrderByDescending(p => p.PostedOn);
        }

        public IQueryable<PostEntity> GetByCategory(string slug)
        {
            return
               _context.Posts
                   .Include(p => p.Category)
                   .Include(p => p.PostTags)
                       .ThenInclude(t => t.Tag)
                   .Where(p => p.Category.UrlSlug == slug)
                   .OrderByDescending(p => p.PostedOn);
        }

        public PostEntity GetById(int id)
        {
            return
               _context.Posts
                    .Where(p => p.Id == id)
                    .SingleOrDefault();
        }
        public PostEntity GetBySlug(string slug)
        {
            return
                _context.Posts
                     .Include(p => p.Category)
                     .Include(p => p.PostTags)
                        .ThenInclude(t => t.Tag)
                     .Where(p => p.UrlSlug == slug)
                     .SingleOrDefault();
        }

        public IQueryable<PostEntity> GetByTags(string slug)
        {
            return
              _context.Posts
                  .Include(p => p.Category)
                  .Include(p => p.PostTags)
                      .ThenInclude(t => t.Tag)
                  .Where(p => p.PostTags.Any(t => t.Tag.Name.ToLower() == slug.ToLower()))
                  .OrderByDescending(p => p.PostedOn);
        }

        public IQueryable<PostEntity> GetLatest(int count)
        {
            return
                _context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.PostTags)
                        .ThenInclude(t => t.Tag)
                    .OrderByDescending(p => p.PostedOn)
                    .Take(count);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public PostEntity Update(PostEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
