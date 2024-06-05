using Backend.Extentions;
using Backend.Interfaces;
using Backend.Mappers;
using Backend.Models;
using Backend.UnitOfWork.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared_ViewModels.Order;
using Shared_ViewModels.Payment;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(UserManager<AppUser> userManager, IOrderUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();

            var order = await _unitOfWork.OrderRepository.GetAllAsync(userId);

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

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

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

            var userId = User.GetUserId();

            var orderModel = await _unitOfWork.OrderRepository.CreateAsync(userId, paymentDto);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = orderModel.Id }, orderModel.ToOrderDto());
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderVmDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderModel = await _unitOfWork.OrderRepository.UpdateAsync(id, updateDto);
            await _unitOfWork.CompleteAsync();

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

            var orderModel = await _unitOfWork.OrderRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            if (orderModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
