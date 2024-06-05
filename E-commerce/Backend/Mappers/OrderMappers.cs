using Backend.Models;
using Shared_ViewModels.Cart;
using Shared_ViewModels.Order;
using Shared_ViewModels.Payment;

namespace Backend.Mappers
{
    public static class OrderMappers
    {
        public static PaymentVmDto ToPaymentDto(this Payment paymentModel)
        {
            return new PaymentVmDto
            {
                Id = paymentModel.Id,
                Amount = paymentModel.Amount,
                PaymentDate = paymentModel.PaymentDate,
                PaymentMethod = paymentModel.PaymentMethod,
                OrderId = paymentModel.OrderId
            };
        }

        public static OrderItemVmDto ToOrderItemDto(this OrderItem orderItemModel)
        {
            return new OrderItemVmDto
            {
                Id = orderItemModel.Id,
                ProductId = orderItemModel.ProductId,
                Quantity = orderItemModel.Quantity,
                UnitPrice = orderItemModel.UnitPrice
            };
        }

        public static OrderVmDto ToOrderDto(this Order orderModel)
        {
            return new OrderVmDto
            {
                Id = orderModel.Id,
                OrderDate = orderModel.OrderDate,
                Status = orderModel.Status,
                TotalAmount = orderModel.TotalAmount,
                AppUserId = orderModel.AppUserId,
                OrderItems = orderModel.OrderItems.Select(s => s.ToOrderItemDto()).ToList(),
                Payment = orderModel.Payment.ToPaymentDto()
            };
        }

        public static Payment ToPaymentCreateDto(this CreatePaymentRequestVmDto paymentDto, decimal TotalAmount)
        {
            return new Payment
            {
                Amount = TotalAmount,
                PaymentDate = DateTime.Now,
                PaymentMethod = paymentDto.PaymentMethod
            };
        }

        public static OrderItem ToOrderItemCreateDto(this CreateOrderItemRequestVmDto orderItemModel)
        {
            return new OrderItem
            {
                ProductId = orderItemModel.ProductId,
                Quantity = orderItemModel.Quantity,
                UnitPrice = orderItemModel.UnitPrice
            };
        }

        public static Order ToOrderFromCreateDTO(this CreateOrderRequestVmDto orderDto)
        {
            decimal TotalAmount = orderDto.OrderItems.Sum(item => item.Quantity * item.UnitPrice);
            return new Order
            {
                Status = orderDto.Status,
                OrderItems = orderDto.OrderItems.Select(o => o.ToOrderItemCreateDto()).ToList(),
                TotalAmount = TotalAmount,
                Payment = orderDto.Payment.ToPaymentCreateDto(TotalAmount),
                OrderDate = DateTime.Now
            };
        }

        public static Order ToOrderFromCart(this Cart cartModel, string userId, CreatePaymentRequestVmDto paymentDto)
        {
            var orderModel = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Checkout",
                TotalAmount = cartModel.TotalAmount,
                AppUserId = userId,
            };

            var paymentModel = new Payment
            {
                Amount = orderModel.TotalAmount,
                PaymentDate = DateTime.Now,
                PaymentMethod = paymentDto.PaymentMethod,
            };

            orderModel.Payment = paymentModel;

            foreach (var item in cartModel.CartItems)
            {
                var orderItemModel = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };

                orderModel.OrderItems.Add(orderItemModel);
            }

            return orderModel;
        }
    }
}
