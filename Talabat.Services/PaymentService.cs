using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Entities.Orders_Aggragtion;
using Talabat.core.Repositiories;
using Product = Talabat.core.Entities.Product;
using Talabat.core.Services;

namespace Talabat.Services
{
    public class PaymentService : IPaymentServices 
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SectertKey"];
            var basket = await basketRepository.GetBasketAsync(basketId);
            if(basket is null) return null;

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);

                shippingPrice = deliveryMethod.Cost;
                basket.ShippingCost=deliveryMethod.Cost;

            }

            if(basket?.items?.Count > 0)
            {
                foreach( var item in basket.items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if(item.Price != product.Price)
                       item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntednId)) //create payment Intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) basket.items.Sum(item => item.Price * item.Quantity * 100) +(long) shippingPrice * 100,

                    Currency="usd",
                    PaymentMethodTypes = new List<string>() {"card"}

                    
                };
                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntednId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else //update paymentIntent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice * 100,

                

                };
                await service.UpdateAsync(basket.PaymentIntednId, options);
            }

            await basketRepository.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
