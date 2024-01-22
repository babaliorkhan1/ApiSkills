using FirstError.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Services.Interfaces
{
    public interface ILogService
    {
        public Task CreateAsync(Log log);
        public Task<IEnumerable<Log>> GetAllAsync();
    }
}
