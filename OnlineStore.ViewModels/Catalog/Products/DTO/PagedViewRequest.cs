using OnlineStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.ViewModels.Catalog.Products.DTO
{
    public class PagedViewRequest : PagingBase
    {
        public string keyword { get; set; }
        public List<int> categoryId { get; set; }
    }
}
