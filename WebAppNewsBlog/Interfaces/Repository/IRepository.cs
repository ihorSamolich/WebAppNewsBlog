namespace WebAppNewsBlog.Interfaces.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T GetBySlug(string slug);
        IQueryable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Delete(int id);
        int Count();
        void Save();
    }
}
