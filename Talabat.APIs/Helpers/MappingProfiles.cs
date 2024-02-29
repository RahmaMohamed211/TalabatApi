using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.core.Entities;


namespace Talabat.APIs.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, productToReturnDto>()
                .ForMember(PD => PD.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
                .ForMember(PD => PD.ProductType, o => o.MapFrom(p => p.ProductType.Name))
                .ForMember(PD => PD.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
            //CreateMap<Address, AddressDto>().ReverseMap();  
            

            CreateMap<CustomerBasketDto, CustomerBasket >();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<core.Entities.identity.Address, AddressDto>().ReverseMap();


            CreateMap<AddressDto, core.Entities.Orders_Aggragtion.Address>();
        }
    }
}
