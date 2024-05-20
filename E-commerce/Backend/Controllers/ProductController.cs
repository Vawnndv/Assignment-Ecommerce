using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Category;
using Shared_ViewModels.Product;
using Shared_ViewModels.Helpers;

namespace Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productRepo.GetAllAsync(query);

            var productsDto = products.Select(s => s.ToProductDto());

            return Ok(productsDto);
        }

        // GET: api/product/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToProductDto());
        }

        // GET: api/product/category/{category}
        [HttpGet("category/{id:int}")]
        public async Task<ActionResult> GetByCategoryId([FromRoute] int id, [FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productRepo.GetByCategoryAsync(id, query);

            var productsDto = products.Select(s => s.ToProductDto());

            return Ok(productsDto);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestVmDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = productDto.ToProductFromCreateDTO();
       
            await _productRepo.CreateAsync(productModel);
            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        // PUT: api/product/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _productRepo.UpdateAsync(id, updateDto);

            if (productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel.ToProductDto());
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _productRepo.DeleteAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
