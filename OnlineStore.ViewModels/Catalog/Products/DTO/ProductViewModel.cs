using Microsoft.EntityFrameworkCore;

namespace OnlineStoreSolution.ViewModels.Catalog.Products.DTO
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Views { get; set; }
        public string SeoAlias { get; set; }
        public DateTime? Date { get; set; }
    }
}
