using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Base
{
    public class PagingBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
