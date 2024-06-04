using Backend.Data;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.UnitOfWork.Category;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Category;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryUnitOfWork _unitOfWork;

        public CategoryController(ICategoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            var categoryDto = categories.Select(s => s.ToCategoryDto());

            return Ok(categoryDto);
        }

        // GET: api/category/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.ToCategoryDto());
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCategoryRequestVmDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = categoryDto.ToCategoryFromCreateDTO();

            await _unitOfWork.CategoryRepository.CreateAsync(categoryModel);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDto());
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = await _unitOfWork.CategoryRepository.UpdateAsync(id, updateDto);
            await _unitOfWork.CompleteAsync();

            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel.ToCategoryDto());
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            if (categoryModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{categoryId}/parent-categories")]
        public async Task<IActionResult> GetParentCategories(int categoryId)
        {
            var parentCategories = await _unitOfWork.CategoryRepository.GetParentCategoriesAsync(categoryId);

            if (parentCategories == null || parentCategories.Count == 0)
            {
                return NotFound("No parent categories found for the given category ID.");
            }

            return Ok(parentCategories.ToListCategoryDto());
        }
    }
}
