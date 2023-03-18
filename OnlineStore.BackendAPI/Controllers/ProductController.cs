using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.ViewModels.Catalog.ProductImage;
using OnlineStoreSolution.App.Catalog.Products;
using OnlineStoreSolution.ViewModels.Catalog.Products;
using OnlineStoreSolution.ViewModels.Catalog.Products.DTO;

namespace OnlineStore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IAdminProductService _adminProductService;
        public ProductController(IPublicProductService publicProductService, IAdminProductService adminProductService)
        {
            _publicProductService = publicProductService;
            _adminProductService = adminProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("public-paging")]
        public async Task<IActionResult> GetByCategoryId([FromQuery]PagedViewRequestPublic request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var product = await _publicProductService.GetByProductId(productId);
            if (product == null)
            {
                return BadRequest("Cannot find product.");
            }
            return Ok(product);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm]CreateProductRequest request)
        {
            var productId = await _adminProductService.Create(request);
            if (productId == null)
            {
                return BadRequest();
            }
            var product = await _publicProductService.GetByProductId(productId);
            return CreatedAtAction(nameof(GetByCategoryId), new {id = productId}, product);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm]UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affected = await _adminProductService.Update(request);
            if (affected == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affected = await _adminProductService.Delete(productId);
            if (affected == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("UpdatePrice/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affected = await _adminProductService.UpdatePrice(productId, newPrice);
            if (affected == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("AddImage/{productId}")]
        public async Task<IActionResult> AddImage(int productId, [FromForm]ProductImageCreateRequest request)
        {
            var imageId = await _adminProductService.AddImage(productId, request);
            if (imageId == null)
            {
                return BadRequest();
            }
            var image = await _adminProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpGet("{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _adminProductService.GetImageById(imageId);
            if (image == null)
            {
                return BadRequest("Cannot find product.");
            }
            return Ok(image);
        }

        [HttpPut("UpdateImage/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]UpdateProductImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affected = await _adminProductService.UpdateImage(imageId, request);
            if (affected == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{imageId}/DeleteImage")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var affected = await _adminProductService.DeleteImage(imageId);
            if (affected == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
