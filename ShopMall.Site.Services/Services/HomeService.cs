using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using ShopMall.Site.Domain.Models;
using ShopMall.Site.Infrastructure.Extensions;

namespace ShopMall.Site.Services.Services
{
    public class HomeService : ServiceBase, IHomeRepositories
    {
        public IRepositoriesBase<MerchantProductDto, Guid> ProductRepositories { protected get; set; }

        private readonly IMapper _mapper;

        public HomeService(IMapper mapper)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// 获取 首页基本数据
        /// </summary>
        /// <param name="merchantId">商户编码</param>
        /// <returns></returns>
        public async Task<HomeModel> HomeEntitiesAsync(int merchantId)
        {
            var sqlFormatter = "SELECT top 6 * FROM V_MerchantProduct WHERE MerchantId=@merchantId AND CategoryId={0}";  

            var drinks= await ProductRepositories.EntitiesAsync(sqlFormatter.FormatWith(1), new { merchantId });
            var snacks = await ProductRepositories.EntitiesAsync(sqlFormatter.FormatWith(2), new { merchantId });
            var cigarettes = await ProductRepositories.EntitiesAsync(sqlFormatter.FormatWith(3), new { merchantId });

            var model = new HomeModel
            {
               ProductCategories=new ProductCategories
               {
                   Drinks = drinks,
                   Snacks = snacks,
                   Cigarettes = cigarettes
               }
            };

            model.Focus = new List<string> {
                "https://www.itying.com/images/flutter/slide01.jpg",
                "https://www.itying.com/images/flutter/slide02.jpg",
                "https://www.itying.com/images/flutter/slide03.jpg"
            };
            model.Recommands.Add(new RecommandModel { 
                ImgUrl= "http://192.168.0.10:8091/drinks.png",
                Title= "饮料/水"
            });
            model.Recommands.Add(new RecommandModel
            {
                ImgUrl = "http://192.168.0.10:8091/snack.png",
                Title = "零食"
            });
            model.Recommands.Add(new RecommandModel
            {
                ImgUrl = "http://192.168.0.10:8091/wine.png",
                Title = "酒水"
            });
            model.Recommands.Add(new RecommandModel
            {
                ImgUrl = "http://192.168.0.10:8091/shopping.png",
                Title = "日用品"
            });
            model.Recommands.Add(new RecommandModel
            {
                ImgUrl = "http://192.168.0.10:8091/cigarette.png",
                Title = "香烟"
            });
            return model;
        }
    }

}
