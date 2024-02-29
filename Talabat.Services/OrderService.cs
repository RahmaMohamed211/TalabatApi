using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Entities.identity;
using Talabat.core.Entities.Orders_Aggragtion;
using Talabat.core.Repositiories;
using Talabat.core.Sepecifitction.Order_Sepc;
using Talabat.core.Services;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenericRepository<Product> productRepo;
        //private readonly IGenericRepository<DeliveryMethod> dmReopistory;
        //private readonly IGenericRepository<core.Entities.Orders_Aggragtion.Order> orderRepository;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            //this.productRepo = productRepo;
            //dmReopistory = DmReopistory;
            //orderRepository = OrderRepository;
        }

        public async Task<core.Entities.Orders_Aggragtion.Order?> CreateOrderAsync(string BuyerEmail, string basketId, int deliveryMethodId, core.Entities.Orders_Aggragtion.Address shippingAddress)
        {

            //1.Get Basket from Basket repo

            var basket = await basketRepository.GetBasketAsync(basketId);

            //2.Get selected item  at basket from productrepo

            var orderItems = new List<OrderItem>();

            if (basket?.items?.Count > 0)
            {
                foreach (var item in basket.items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                    var productItemOrdered = new ProductOrderItem(product.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);


                    orderItems.Add(orderItem);




                }
            }

            //3.calculate sub total
            var subTotal = orderItems.Sum(item=>item.Price *item.Quantity);

            //4.Get delivery method from dm repository
            var deliverymethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //5.create order
            var Order = new core.Entities.Orders_Aggragtion.Order(BuyerEmail, shippingAddress, deliverymethod, orderItems, subTotal);

            //6.Add order locally
            await unitOfWork.Repository<core.Entities.Orders_Aggragtion.Order>().Add(Order); //locally

            //7.save order to database (orders)
            var result=await unitOfWork.Complete();
            if (result <= 0) return null;

            return Order;



        }

        public async Task<IReadOnlyList<core.Entities.Orders_Aggragtion.Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var Orders = await unitOfWork.Repository<core.Entities.Orders_Aggragtion.Order>().GetAllwithSpecAsync(spec);

            return Orders;
        }
        public async Task<core.Entities.Orders_Aggragtion.Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)

        {
            var spec =new OrderSpecification(orderId,buyerEmail);
            var Order = await unitOfWork.Repository<core.Entities.Orders_Aggragtion.Order>().GetByIdwithSpecAsync(spec);

            return Order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var delivaryMethods = await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return delivaryMethods;
        }

       
    }
}
