using OnlineStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.System
{
    public class GetUserPagingRequest : PagingBase
    {
        public string? Keyword { get; set; }
    }
}
