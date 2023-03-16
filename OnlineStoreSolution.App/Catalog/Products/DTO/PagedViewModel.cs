using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Catalog.Products.DTO
{
    public class PagedViewModel<T>
    {
        public List<T> products { get; set; }
        public int totalRecords { get; set; }
    }
}
