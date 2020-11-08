using AutoMapper;
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities; 

namespace ShopMall.Site.Automapper.Profiles
{
    public class MerchantProductProfile:Profile
    {
        public MerchantProductProfile()
        {
            CreateMap<MerchantProduct, MerchantProductDto>(); 
        }
    }
}
