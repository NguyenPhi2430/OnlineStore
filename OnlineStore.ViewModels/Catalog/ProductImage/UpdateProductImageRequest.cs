using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels.Catalog.ProductImage
{
    public class UpdateProductImageRequest
    {
        public IFormFile ImageFile { get; set; }
        public bool IsDefault { get; set; }
        public string Caption { get; set; }
    }
}
