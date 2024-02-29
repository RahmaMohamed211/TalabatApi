﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Orders_Aggragtion;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());//1 to 1 total 

            builder.Property(O => O.Status)
                .HasConversion(
                  Os => Os.ToString(),
                  OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");


            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);



        }
    }
}
