using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using OnlineShop.Ultilities.Exceptions;
using OnlineStore.ViewModels.Base;
using OnlineStore.ViewModels.Catalog.ProductImage;
using OnlineStoreSolution.App.Base;
using OnlineStoreSolution.Data.EF_Core;
using OnlineStoreSolution.Data.Entities;
using OnlineStoreSolution.ViewModels.Catalog.Products;
using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;
using System.Data.Entity;
using System.Net.Http.Headers;

namespace OnlineStoreSolution.App.Catalog.Products
{
    public class AdminProductService : IAdminProductService
    {
        private readonly OnlineStoreDBContext _context;
        private readonly IStorageService _storageService;

        public AdminProductService(OnlineStoreDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(CreateProductRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Stock = request.Stock,
                SeoAlias = "SeoAlias",
                Views = 0,
                Date = DateTime.Now,
            };
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail Image",
                        CreateDate = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1,
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
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
            if (product.ProductImages != null)
            {
                var images = _context.ProductImages.Where(x=> x.Id == productId);
                foreach (var image in images)
                {
                    await _storageService.DeleteFileAsync(image.ImagePath);
                }
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedViewModel<ProductViewModel>> GetAllPaging(PagedViewRequestAdmin request)
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
            if (request.ThumbnailImage != null)
            {
                var thumbnail = await _context.ProductImages.FirstOrDefaultAsync(x=>x.IsDefault == true &&  x.Id == request.Id);
                if (thumbnail != null)
                {
                    thumbnail.FileSize = request.ThumbnailImage.Length;
                    thumbnail.ImagePath = await this.SaveFile(request.ThumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFilename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFilename)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (request.ImageFile == null) {
                throw new OnlineShopExceptions("Cannot find product or file does not exit.");
            }
            var productImage = new ProductImage()
             {
                ProductId = productId,
                Caption = request.Caption,
                CreateDate = DateTime.Now,
                FileSize = request.ImageFile.Length,
                SortOrder = request.SortOrder,
                IsDefault = request.IsDefault,
                ImagePath = await this.SaveFile(request.ImageFile),
             };
            product.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> DeleteImage(int imageId)
        {
            var image = await (from pi in _context.ProductImages
                         where pi.Id == imageId
                         select pi).FirstOrDefaultAsync();
            if (image == null)
            {
                throw new OnlineShopExceptions("No image found");
            }
            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, UpdateProductImageRequest request)
        {
            var image = await (from pi in _context.ProductImages
                              where pi.Id == imageId
                              select pi).FirstOrDefaultAsync();
            if (image == null)
            {
                throw new OnlineShopExceptions("No image found");
            }
            image.FileSize = request.ImageFile.Length;
            image.ImagePath = await this.SaveFile(request.ImageFile);
            image.Caption = request.Caption;
            image.IsDefault = request.IsDefault;
            _context.ProductImages.Update(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) {
                throw new OnlineShopExceptions("No product found");
            }
            var imageList = new List<ProductImageViewModel>();
            foreach (var image in product.ProductImages)
            {
                var imageView = new ProductImageViewModel()
                {
                    Id = image.Id,
                    ProductId = image.ProductId,
                    ImagePath = image.ImagePath,
                    Caption = image.Caption,
                    IsDefault = image.IsDefault,
                    CreateDate = image.CreateDate,
                    SortOrder = image.SortOrder,
                    FileSize = image.FileSize,
                };
                imageList.Add(imageView);
            }
            return imageList;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
            {
                throw new OnlineShopExceptions("No product found");
            }
            var imageView = new ProductImageViewModel()
            {
                Id = image.Id,
                ProductId = image.ProductId,
                ImagePath = image.ImagePath,
                Caption = image.Caption,
                IsDefault = image.IsDefault,
                CreateDate = image.CreateDate,
                SortOrder = image.SortOrder,
                FileSize = image.FileSize,
            };
            return imageView;
        }
    }

    }

