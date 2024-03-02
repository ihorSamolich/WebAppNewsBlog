using Microsoft.EntityFrameworkCore;
using WebAppNewsBlog.Data;
using WebAppNewsBlog.Data.Entities;
using WebAppNewsBlog.Interfaces.Repository;

namespace WebAppNewsBlog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppEFContext _context;

        public CategoryRepository(AppEFContext context)
        {
            _context = context;
        }

        public CategoryEntity Add(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return _context.Categories.Count();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CategoryEntity> GetAll()
        {
            return _context.Categories;
        }

        public CategoryEntity GetById(int id)
        {
            return
                _context.Categories
                     .Where(p => p.Id == id)
                     .SingleOrDefault();
        }

        public CategoryEntity GetBySlug(string slug)
        {
            return
                _context.Categories
                     .Where(p => p.UrlSlug == slug)
                     .SingleOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public CategoryEntity Update(CategoryEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
