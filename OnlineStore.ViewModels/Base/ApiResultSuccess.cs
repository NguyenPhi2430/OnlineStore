using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Base
{
    public class ApiResultSuccess<T> : ApiResult<T>
    {
        public ApiResultSuccess() {
            IsSuccess = true;
        }

        public ApiResultSuccess(T resultObject)
        {
            IsSuccess = true;
            ResultObject = resultObject;
        }
    }
}
