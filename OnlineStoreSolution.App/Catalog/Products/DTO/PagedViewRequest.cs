using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Catalog.Products.DTO
{
    public class PagedViewRequest : PagingBase
    {
        public string keyword { get; set; }
        public int categoryId { get; set; }
    }
}
