﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; }

        public string? PaymentIntednId { get; set; }
        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal ShippingCost { get; set; }
    }
}
