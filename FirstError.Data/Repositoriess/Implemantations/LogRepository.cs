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
    public  class LogRepository:Repository<Log>,ILogRepository
    {
        public LogRepository(ApiDbContext apiDbContext):base(apiDbContext)
        {

        }
    }
}
