using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Views { get; set; }
        public string SeoAlias { get; set; }
        public DateTime? Date { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
