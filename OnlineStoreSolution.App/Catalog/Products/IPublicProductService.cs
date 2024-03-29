﻿using OnlineStore.ViewModels.Base;
using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PagedViewModel<ProductViewModel>> GetAllByCategoryId(PagedViewRequestPublic request);
        Task<List<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByProductId(int id);
    }
}
