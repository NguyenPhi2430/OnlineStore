using Microsoft.AspNetCore.Http;

namespace OnlineStoreSolution.ViewModels.Catalog.Products.DTO
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Views { get; set; }
        public DateTime? Date { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
