using Microsoft.AspNetCore.Http;
using OnlineStore.ViewModels.Base;
using OnlineStore.ViewModels.Catalog.ProductImage;
using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Catalog.Products
{
    public interface IAdminProductService
    {
        Task<int> Create(CreateProductRequest request);
        Task<int> Update(UpdateProductRequest request);
        Task<bool> UpdateStock(int productId, int newStock);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task UpdateView(int productId);
        Task<int> Delete(int productId);
        Task<PagedViewModel<ProductViewModel>> GetAllPaging(PagedViewRequestAdmin request);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> DeleteImage(int imageId);
        Task<int> UpdateImage(int imageId, UpdateProductImageRequest request);
        Task<List<ProductImageViewModel>> GetListImages (int productId);
        Task<ProductImageViewModel> GetImageById (int imageId);
    }
}
