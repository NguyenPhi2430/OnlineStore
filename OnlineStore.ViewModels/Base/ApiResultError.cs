using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Base
{
    public class ApiResultError<T> : ApiResult<T>
    {
        public ApiResultError() {
            IsSuccess = false;
        }

        public ApiResultError(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }
}
