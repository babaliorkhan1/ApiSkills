using FirstApi.Data.Contexts;
using FirstApi.Core.Entities;
using FirstApi.Core.Repositories1;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FirstApi.Data.Repositories.Implemantations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApiDbContext _apiDbContext;
        public CategoryRepository(ApiDbContext apiDbContext):base(apiDbContext)
        {
            //_apiDbContext.Categories.CountAsync();sayi verir
            //_apiDbContext.Categories.AnyAsync();olub olmaidigini kontrol edib bool result return edir
            //_apiDbContext.Categories.Distinct();bize eyni olan deyerlerden birin getirib o halda getirir datalari
            //_apiDbContext.Categories.AllAsync();butun datalar uzre yoxlayr verdiyimiz sarti eger hepsi
            //oduyorsa true other way false return edir
            //_apiDbContext.Categories.Where(x => x.Name.Contains("s"));
            //like sorgusu olsuturmak icin bizim yapmali
            //gerekdigimiz sorgu yani contains where icersiinde
            //olmalidi tipki sql like isletdiyimizde whereden sonra contains isletdiyimiz gibi
            //_apiDbContext.Categories.ToDictionary();tolistden ferqi key value olarag getiriri deyerleri
            //============================================================================================
            //var result = await _apiDbContext.Categories.ToArrayAsync();
            //ayni commandlari ishlediyor ama return edeerken uygulamada biri liste biri ize dizi getirir
            //dizi sabit boyuta ve cox islevsellige sahib deyil deye tolist bazi yerlerde kullanislidir
            //ama 

            //var result1 = await _apiDbContext.Categories.ToListAsync();
            //_apiDbContext.Categories.Select(x => new

            //{
            //    Name = x.Name,

            //}).ToList();

            //iilisikisel verilerde selectmany islediyoruz select o anda islemiyor peki nasilmi>?
            //var result = await _apiDbContext.Categories.Include(x => x.Products).SelectMany(x => x.Products, (x, p) => new
            //{
            //    x.Id,
            //    x.CreatedTime,
            //    p.Name
            //}).ToListAsync();
           


        }

        //Tolist methodunun bashka shkeilde yazilmasinin bu yontemide var===
        //var categoriess=await (from categories in _apiDbContext.Categories select categories).ToListAsync(); 




    }
}
