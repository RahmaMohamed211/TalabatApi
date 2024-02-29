using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentServices paymentServices;

        public PaymentController(IPaymentServices paymentServices)
        {
            this.paymentServices = paymentServices;
        }
        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")] //post : /api/payment?id=basketif
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket =await paymentServices.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiErrorResponse(400, "A Problem with your basket"));

            return Ok(basket);
        
        }
    }
}
