using Backend.Extentions;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.UnitOfWork.Cart;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Cart;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public CartController(UserManager<AppUser> userManager, ICartUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var cart = await _unitOfWork.CartRepository.GetAllAsync(appUser);

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

            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);

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

            await _unitOfWork.CartRepository.CreateAsync(cartModel);
            await _unitOfWork.CompleteAsync();

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

            var cartModel = await _unitOfWork.CartRepository.UpdateAsync(updateDto, appUser);
            await _unitOfWork.CompleteAsync();

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

            var cartModel = await _unitOfWork.CartRepository.DeleteAsync(appUser);
            await _unitOfWork.CompleteAsync();

            if (cartModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
