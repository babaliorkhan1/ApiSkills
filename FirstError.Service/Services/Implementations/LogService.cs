using FirstError.Core.Entities;
using FirstError.Core.Repositories1;
using FirstError.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Services.Implementations
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;
        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }
        

        public async Task CreateAsync(Log log)
        {
            await _repository.AddAsync(log);
          await  _repository.SaveAsync();
            
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
