using FirstApi.Data.Contexts;
using FirstApi.Data.Repositories.Implemantations;
using FirstError.Core.Entities;
using FirstError.Core.Repositories1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Data.Repositoriess.Implemantations
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        private readonly ApiDbContext _apiDbContext;
        public ProductRepository( ApiDbContext apiDbContext):base(apiDbContext)
        {
            //_apiDbContext.Categories.Where(x => x.IsDeleted).
            //OrderBy(x => x.CreatedTime)
            //.ThenBy(x => x.UpdatedTime);
            //egerki createdtime eyni olursa uopdate time gore orderby edecekdir
            //apiDbContext.Categories.OrderByDescending(x => x.CreatedTime).ThenByDescending(x => x.UpdatedTime);
        }
    }
}
