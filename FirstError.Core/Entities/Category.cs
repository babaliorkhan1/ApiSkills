using FirstError.Core.Entities;
using FirstError.Core.Entities.Base;
using System.Globalization;

namespace FirstApi.Core.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
