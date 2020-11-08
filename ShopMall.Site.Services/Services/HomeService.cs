using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using ShopMall.Site.Domain.Models;

namespace ShopMall.Site.Services.Services
{
    public class HomeService : ServiceBase, IHomeRepositories
    {
        public IRepositoriesBase<MerchantProduct, Guid> MProductRepositories { protected get; set; }

        private readonly IMapper _mapper;

        public HomeService(IMapper mapper)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<HomeModel> HomeEntitiesAsync(int merchantId)
        {
           var products= await MProductRepositories.EntitiesAsync("p_MerchantProduct", new { merchantId }, commandType:System.Data.CommandType.StoredProcedure);

            return new HomeModel { 
                Products = _mapper.Map<IEnumerable<MerchantProductDto>>(products) };
        }
    }
}
