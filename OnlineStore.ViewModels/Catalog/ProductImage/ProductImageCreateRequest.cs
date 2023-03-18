using Microsoft.AspNetCore.Http;

namespace OnlineStoreSolution.App.Catalog.Products
{
    public class ProductImageCreateRequest
    {
        public string Caption { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}