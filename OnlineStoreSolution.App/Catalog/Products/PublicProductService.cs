using OnlineStoreSolution.App.Catalog.Products.DTO;
using OnlineStoreSolution.Data.EF_Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly OnlineStoreDBContext _context;

        public PublicProductService(OnlineStoreDBContext context)
        {
            _context = context;
        }
        public async Task<PagedViewModel<ProductViewModel>> GetAllByCategoryId(PagedViewRequest request)
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
