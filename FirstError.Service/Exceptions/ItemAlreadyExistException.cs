using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Exceptions
{
    public class ItemAlreadyExistException:Exception
    {
        public ItemAlreadyExistException(string msh):base(msh)
        {

        }
        public string Name { get; set; }
    }
}
