using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public List<BasketItem> items { get; set; }

        public string PaymentIntednId { get; set; }

        public string ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal ShippingCost { get; set; }
        public CustomerBasket(string id)
        {
            Id= id;
        }
    }
}
