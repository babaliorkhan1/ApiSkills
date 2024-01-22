using FirstError.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Core.Entities
{
    public  class Log:BaseEntity
    {
        public  string UserId { get; set; } 
        public string Action { get; set; }  
    }
}
