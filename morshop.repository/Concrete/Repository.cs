using Microsoft.EntityFrameworkCore;
using morshop.repository.Abstract;

namespace morshop.repository.Concrete
{
    public class Repository<T> : IRepository<T>
    where T:class
    {
        protected readonly DbContext context;
        public Repository(DbContext ctx)
        {
            context=ctx;
        }
        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }

        public async Task CreateAsync(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {

            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteeAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<List<T>> GeTAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State=EntityState.Modified;
            context.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State=EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}