using Backend.Extentions;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Product;

namespace Backend.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepo;
        private readonly UserManager<AppUser> _userManager;

        public CartController(UserManager<AppUser> userManager, ICartRepository cartRepo)
        {
            _userManager = userManager;
            _cartRepo = cartRepo;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var cart = await _cartRepo.GetAllAsync(appUser);

            if (cart == null)
            {
                return NotFound();
            }

            var cartDto = cart.ToCartDto();

            return Ok(cartDto);
        }

        // GET: api/cart/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = await _cartRepo.GetByIdAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart.ToCartDto());
        }

        // POST: api/cart
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCartRequestVmDto cartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var cartModel = cartDto.ToCartFromCreateDTO(appUser);

            await _cartRepo.CreateAsync(cartModel);
            return CreatedAtAction(nameof(GetById), new { id = cartModel.Id }, cartModel.ToCartDto());
        }

        // PUT: api/cart
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCartVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var cartModel = await _cartRepo.UpdateAsync(updateDto, appUser);

            if (cartModel == null)
            {
                return NotFound();
            }

            return Ok(cartModel.ToCartDto());
        }

        // DELETE: api/cart
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var productModel = await _cartRepo.DeleteAsync(appUser);

            if (productModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
