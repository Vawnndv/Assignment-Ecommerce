using Backend.Extentions;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Order;
using Shared_ViewModels.Payment;

namespace Backend.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(UserManager<AppUser> userManager, IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
            _userManager = userManager;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var order = await _orderRepo.GetAllAsync(appUser);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = order.Select(s => s.ToOrderDto());

            return Ok(orderDto);
        }

        // GET: api/order/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderRepo.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order.ToOrderDto());
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePaymentRequestVmDto paymentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var orderModel = await _orderRepo.CreateAsync(appUser, paymentDto);
            return CreatedAtAction(nameof(GetById), new { id = orderModel.Id }, orderModel.ToOrderDto());
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var orderModel = await _orderRepo.UpdateAsync(id, updateDto);

            if (orderModel == null)
            {
                return NotFound();
            }

            return Ok(orderModel.ToOrderDto());
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderModel = await _orderRepo.DeleteAsync(id);

            if (orderModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
