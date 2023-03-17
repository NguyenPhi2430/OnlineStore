using Microsoft.AspNetCore.Http;
using OnlineStore.ViewModels.Base;
using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.ViewModels.Catalog.Products
{
    public interface IAdminProductService
    {
        Task<int> Create(CreateProductRequest request);
        Task<int> Update(UpdateProductRequest request);
        Task<bool> UpdateStock(int productId, int newStock);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task UpdateView(int productId);
        Task<int> Delete(int productId);
        Task<PagedViewModel<ProductViewModel>> GetAllPaging(PagedViewRequest request);
        Task<int> AddImages(int productId, List<IFormFile> images);
        Task<int> DeleteImages(int productId);
        Task<int> UpdateImage(int imageId, string caption, bool IsDefault);
    }
}
