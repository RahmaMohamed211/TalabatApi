﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.Orders_Aggragtion
{
    public class Order:BaseEntity
    {
        //must be parmaterless Constructor
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }

        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } //navegtionl prop [one]


        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }


        public decimal GetTotal()
        => SubTotal + DeliveryMethod.Cost;


        public string? PaymentIntentId { get; set; }





    }



    }

