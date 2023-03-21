using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Base
{
    public class PagedViewModel<T>
    {
        public List<T> items { get; set; }
        public int totalRecords { get; set; }
    }
}
