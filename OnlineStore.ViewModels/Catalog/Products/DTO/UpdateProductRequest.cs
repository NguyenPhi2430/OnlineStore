using Microsoft.AspNetCore.Http;

namespace OnlineStoreSolution.ViewModels.Catalog.Products.DTO
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}