﻿using Backend.Extentions;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.UnitOfWork.ProductRating;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IProductRatingUnitOfWork _productRatingUnitOfWork;

        public ProductRatingController(UserManager<AppUser> userManager, IProductRatingUnitOfWork productRatingUnitOfWork)
        {
            _userManager = userManager;
            _productRatingUnitOfWork = productRatingUnitOfWork;
        }

        // GET: api/productrating
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int productId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productRatings = await _productRatingUnitOfWork.ProductRatingRepository.GetAllByProductIdAsync(productId);

            var productRatingDto = productRatings.Select(s => s.ToProductRatingDto());

            return Ok(productRatingDto);
        }

        // GET: api/productrating/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productRating = await _productRatingUnitOfWork.ProductRatingRepository.GetByIdAsync(id);

            if (productRating == null)
            {
                return NotFound();
            }

            return Ok(productRating.ToProductRatingDto());
        }

        // POST: api/productrating
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] CreateProductRatingRequestVmDto productRatingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();

            var productRatingModel = productRatingDto.ToProductRatingFromCreateDTO(userId);

            await _productRatingUnitOfWork.ProductRatingRepository.CreateAsync(productRatingModel);
            await _productRatingUnitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = productRatingModel.Id }, productRatingModel.ToProductRatingDto());
        }

        // PUT: api/productrating/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRatingVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();

            var productRatingModel = await _productRatingUnitOfWork.ProductRatingRepository.UpdateAsync(userId, id, updateDto);

            if (productRatingModel == null)
            {
                return NotFound();
            }

            await _productRatingUnitOfWork.CompleteAsync();
            return Ok(productRatingModel.ToProductRatingDto());
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productRatingModel = await _productRatingUnitOfWork.ProductRatingRepository.DeleteAsync(id);

            if (productRatingModel == null)
            {
                return NotFound();
            }

            await _productRatingUnitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
