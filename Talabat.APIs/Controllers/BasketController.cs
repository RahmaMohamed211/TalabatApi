using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.core.Entities;
using Talabat.core.Repositiories;

namespace Talabat.APIs.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id}")] //Get:api/basket/id
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);

            return basket is null ? new CustomerBasket(id) : basket;


        }
        [HttpPost] //post :api/basket
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket=mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var CreateOrUpdatebasket = await basketRepository.UpdateBasketAsync(mappedBasket);
            if(CreateOrUpdatebasket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(CreateOrUpdatebasket);
        }
        [HttpDelete] //delete :api/basket
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
           return  await basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
