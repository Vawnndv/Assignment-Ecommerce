using Backend.Interfaces;
using Backend.Mappers;
using Backend.UnitOfWork.Product;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Helpers;
using Shared_ViewModels.Product;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductUnitOfWork _unitOfWork;

        public ProductController(IProductUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _unitOfWork.ProductRepository.GetAllAsync(query);

            var productsDto = products.Select(s => s.ToProductDto());

            return Ok(productsDto);
        }

        // GET: api/product/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

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

            var products = await _unitOfWork.ProductRepository.GetByCategoryAsync(id, query);

            var productsDto = products.Select(s => s.ToProductDto());

            return Ok(productsDto);
        }

        // GET: api/product/category/numberpages{category}
        [HttpGet("category/numberpages/{id:int}")]
        public async Task<ActionResult> GetNumOfProductPagesByCategory([FromRoute] int id, [FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var num = await _unitOfWork.ProductRepository.GetNumOfProductPagesByCategory(id, query);

            return Ok(num);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestVmDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = productDto.ToProductFromCreateDTO();

            await _unitOfWork.ProductRepository.CreateAsync(productModel);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        // PUT: api/product/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _unitOfWork.ProductRepository.UpdateAsync(id, updateDto);

            if (productModel == null)
            {
                return NotFound();
            }

            await _unitOfWork.CompleteAsync();

            return Ok(productModel.ToProductDto());
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _unitOfWork.ProductRepository.DeleteAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
