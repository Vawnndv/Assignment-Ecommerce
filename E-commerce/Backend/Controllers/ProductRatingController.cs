using Backend.Extentions;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Product;

namespace Backend.Controllers
{
    [Route("api/productrating")]
    [ApiController]
    public class ProductRatingController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRatingRepository _productRatingRepo;

        public ProductRatingController(UserManager<AppUser> userManager, IProductRatingRepository productRatingRepo)
        {
            _userManager = userManager;
            _productRatingRepo = productRatingRepo;
        }

        // GET: api/productrating
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productRatings = await _productRatingRepo.GetAllByProductIdAsync(productId);

            var productRatingDto = productRatings.Select(s => s.ToProductRatingDto());

            return Ok(productRatingDto);
        }

        // GET: api/productrating/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productRating = await _productRatingRepo.GetByIdAsync(id);

            if (productRating == null)
            {
                return NotFound();
            }

            return Ok(productRating.ToProductRatingDto());
        }

        // POST: api/productrating
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductRatingRequestVmDto productRatingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var productRatingModel = productRatingDto.ToProductRatingFromCreateDTO(appUser);

            await _productRatingRepo.CreateAsync(productRatingModel);
            return CreatedAtAction(nameof(GetById), new { id = productRatingModel.Id }, productRatingModel.ToProductRatingDto());
        }

        // PUT: api/productrating/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRatingVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var productRatingModel = await _productRatingRepo.UpdateAsync(appUser, id, updateDto);

            if (productRatingModel == null)
            {
                return NotFound();
            }

            return Ok(productRatingModel.ToProductRatingDto());
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productRatingModel = await _productRatingRepo.DeleteAsync(id);

            if (productRatingModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
