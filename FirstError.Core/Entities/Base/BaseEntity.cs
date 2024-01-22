using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Core.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; } 
        public  bool IsDeleted { get; set; }    
        public DateTime CreatedTime { get; set; }   
        public DateTime UpdatedTime { get; set;}
    }
}
