using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Ultilities.Exceptions
{
    public class OnlineShopExceptions : Exception
    {
        public OnlineShopExceptions(string? message) : base(message)
        {
        }
    }
}
