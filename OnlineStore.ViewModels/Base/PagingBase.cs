using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Base
{
    public class PagingBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
