using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;
using OnlineStoreSolution.Data.EF_Core;
using Microsoft.EntityFrameworkCore;

using OnlineStore.ViewModels.Base;
using Azure.Core;

namespace OnlineStoreSolution.ViewModels.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly OnlineStoreDBContext _context;

        public PublicProductService(OnlineStoreDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var query = from p in _context.Products
                        select new { p };

            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Description = x.p.Description,
                SeoAlias = x.p.SeoAlias,
                Date = x.p.Date,
                Views = x.p.Views,
                Stock = x.p.Stock,
                Price = x.p.Price,
            }).ToListAsync();
            return data;
        }

        public async Task<PagedViewModel<ProductViewModel>> GetAllByCategoryId(PagedViewRequestPublic request)
        {
            // Select
            var query = from p in _context.Products
                        join c in _context.Categories on p.Id equals c.Id
                        join pic in _context.ProductInCategories on c.Id equals pic.ProductId
                        select new { p, c, pic };

            if (request.categoryId != null && request.categoryId > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.categoryId);
            }

            //Paging
            int totalRows = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                                    .Select(x => new ProductViewModel()
                                    {
                                        Id = x.p.Id,
                                        Name = x.p.Name,
                                        Description = x.p.Description,
                                        SeoAlias = x.p.SeoAlias,
                                        Date = x.p.Date,
                                        Views = x.p.Views,
                                        Stock = x.p.Stock,
                                        Price = x.p.Price,
                                    }).ToListAsync();

            //Paging Result
            var pagedViewModel = new PagedViewModel<ProductViewModel>()
            {
                products = data,
                totalRecords = totalRows,
            };
            return pagedViewModel;
        }
    }
}
