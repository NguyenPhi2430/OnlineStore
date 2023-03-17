using OnlineStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.ViewModels.Catalog.Products.DTO
{
    public class PagedViewRequestPublic : PagingBase
    {
        public int categoryId { get; set; }
    }
}
