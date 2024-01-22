using FirstApi.Data.Contexts;
using FirstApi.Core.Entities;
using FirstApi.Core.Repositories1;

namespace FirstApi.Data.Repositories.Implemantations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApiDbContext _apiDbContext;
        public CategoryRepository(ApiDbContext apiDbContext):base(apiDbContext)
        {
        }


    }
}
