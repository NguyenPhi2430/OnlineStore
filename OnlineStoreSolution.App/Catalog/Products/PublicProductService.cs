﻿using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;
using OnlineStoreSolution.Data.EF_Core;
using Microsoft.EntityFrameworkCore;

using OnlineStore.ViewModels.Base;


namespace OnlineStoreSolution.App.Catalog.Products
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
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
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
                items = data,
                totalRecords = totalRows,
            };
            return pagedViewModel;
        }

        public async Task<ProductViewModel> GetByProductId(int id)
        {
            var product = await (from p in _context.Products
                          where p.Id == id
                          select p).FirstOrDefaultAsync();
            var result = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SeoAlias = product.SeoAlias,
                Date = product.Date,
                Views = product.Views,
                Stock = product.Stock,
                Price = product.Price,
            };
            return result;
        }
    }
}
