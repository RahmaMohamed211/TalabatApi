using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.Orders_Aggragtion
{//oI =>POI => 1 to 1 Total
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }



        public ProductOrderItem Product { get; set; }

        public decimal  Price { get; set; }

        public int Quantity { get; set; }

    }
}
