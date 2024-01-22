using FirstApi.Data.Contexts;
using FirstApi.Core.Repositories1;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;

namespace FirstApi.Data.Repositories.Implemantations
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApiDbContext apiDbContextt;
        public Repository(ApiDbContext apiDbContext)
        {
            apiDbContextt= apiDbContext;
        }
        //Her iki yöntem de belirli bir varlık türünü bir DbSet'e eklemek için kullanılabilir,
        //ancak Set<T>() metodunun kullanımı,
        //bu işlemi biraz daha spesifik ve hızlı bir şekilde gerçekleştirmenizi sağlar. 

        public  async Task AddAsync(T entity)
        {
            await apiDbContextt.Set<T>().AddAsync(entity);   
        }

        public async  Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression, params string[] includes )
        {
            var query = apiDbContextt.Set<T>().Where(expression);
            if (includes!=null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
           

            return query;
        }

        public async Task<T> GetBYId(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = apiDbContextt.Set<T>().Where(expression);
             
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.FirstOrDefaultAsync();
        } 

        public  async Task<bool> IsExist(Expression<Func<T, bool>> expression)
        {
            return    apiDbContextt.Set<T>().Where(expression).Count() > 0;    
        }

        public async Task Remove(T entity)
        {
           apiDbContextt.Remove(entity);    
        }

        public int Save()
        {
            return apiDbContextt.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await apiDbContextt.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            apiDbContextt.Update(entity);   
        }

       
    }
}
