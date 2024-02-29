using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;

using Talabat.core.Services;
using Talabat.core.Entities.Orders_Aggragtion;


namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        [ProducesResponseType(typeof(core.Entities.Orders_Aggragtion.Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]//post:/api/orders
        public async Task<ActionResult<core.Entities.Orders_Aggragtion.Order>> CreateOrder(OrderDto orderDto)
        {
          var buyerEmail =User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<AddressDto, core.Entities.Orders_Aggragtion.Address>(orderDto.ShippingAdress);

            var Order = await orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId,orderDto.DeliveryMethodId,address);

            if (Order is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(Order);

        }

        [HttpGet] //get://api/orders
        public async Task<ActionResult<IReadOnlyList<core.Entities.Orders_Aggragtion.Order>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrdersForUserAsync(buyerEmail);

            return Ok(Orders);
        }
        [ProducesResponseType(typeof(core.Entities.Orders_Aggragtion.Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] //get :api/orders/1
        public async Task<ActionResult<core.Entities.Orders_Aggragtion.Order>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            if (Order is null) return NotFound(new ApiErrorResponse(404));

            return Ok(Order);
        }
        [HttpGet("delivarymethods")]//get:api/order/delivarymethods

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDelivaryMethod()
        {
            var delivaryMethod = await orderService.GetDeliveryMethodsAsync();
            return Ok(delivaryMethod);
        }

    }
}
