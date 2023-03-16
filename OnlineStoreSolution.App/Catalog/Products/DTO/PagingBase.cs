using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Catalog.Products.DTO
{
    public class PagingBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
