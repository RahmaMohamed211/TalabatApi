using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Talabat.core.Sepecifitction.Order_Sepc
{
    public class OrderSpecification :BaseSpecification<core.Entities.Orders_Aggragtion.Order>
    {
        public OrderSpecification(string email):base(O=>O.BuyerEmail==email)
        {
            includes.Add(O => O.DeliveryMethod);
            includes.Add(O => O.Items);

            AddOrderByDescanding(O => O.OrderDate);

            
        }
        public OrderSpecification(int id,string email) : base(O => O.BuyerEmail == email&&O.Id==id)
        {
            includes.Add(O => O.DeliveryMethod);
            includes.Add(O => O.Items);

           


        }
    }
}
