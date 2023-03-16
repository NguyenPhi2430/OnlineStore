using OnlineShop.Ultilities.Exceptions;
using OnlineStoreSolution.App.Catalog.Products.DTO;
using OnlineStoreSolution.Data.EF_Core;
using OnlineStoreSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineStoreSolution.App.Catalog.Products
{
    public class AdminProductService : IAdminProductService
    {
        private readonly OnlineStoreDBContext _context;

        public AdminProductService(OnlineStoreDBContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CreateProductRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Stock = request.Stock,
                Views = 0,
                Date = DateTime.Now,
            };
            _context.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStock(int productId, int newStock)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new OnlineShopExceptions($"Can't fint Product with Id: {productId}");
            if (product == null) { }
            product.Stock = newStock;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new OnlineShopExceptions($"Can't fint Product with Id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateView(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new OnlineShopExceptions($"Can't fint Product with Id: {productId}");
            product.Views += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new OnlineShopExceptions($"Can't fint Product with Id: {productId}");
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedViewModel<ProductViewModel>> GetAllPaging(PagedViewRequest request)
        {
            // Select
            var query = from p in _context.Products
                        join c in _context.Categories on p.Id equals c.Id
                        join pic in _context.ProductInCategories on c.Id equals pic.ProductId
                        select new {p,c,pic};

            if (request.categoryId.Count > 0)
            {
                query = query.Where(p => request.categoryId.Contains(p.pic.CategoryId));
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

        public async Task<int> Update(UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            return await _context.SaveChangesAsync();
        }
    }
}
