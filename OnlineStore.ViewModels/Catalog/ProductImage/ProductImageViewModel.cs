namespace OnlineStoreSolution.App.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public long FileSize { get; set; }
    }
}