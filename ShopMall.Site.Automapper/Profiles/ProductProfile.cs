using AutoMapper;
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities;

namespace ShopMall.Site.Automapper.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
