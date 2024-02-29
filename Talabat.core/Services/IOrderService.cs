using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Orders_Aggragtion;

namespace Talabat.core.Services
{
    public interface IOrderService
    {
        Task<Entities.Orders_Aggragtion.Order?> CreateOrderAsync(string BuyerEmail,string basketId,int deliveryMethodId,Address shippingAddress);

        Task<IReadOnlyList<Entities.Orders_Aggragtion.Order>> GetOrdersForUserAsync(string buyerEmail);

        Task<Entities.Orders_Aggragtion.Order> GetOrderByIdForUserAsync(int orderId,string buyerEmail);


        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

    }
}
