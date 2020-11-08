using System;
using System.Collections.Generic;
using System.Text;
using ShopMall.Site.Domain.Dtos; 

namespace ShopMall.Site.Domain.Models
{
    public class HomeModel
    {
        /// <summary>
        /// 首页展示的商品分类
        /// </summary>
        public ProductCategories ProductCategories { get; set; }

        /// <summary>
        /// 轮播图
        /// </summary>
        public IEnumerable<string> Focus { get; set; }

        /// <summary>
        /// 推荐列表
        /// </summary>
        public List<RecommandModel> Recommands { get; set; } = new List<RecommandModel>();
    }

    public class ProductCategories
    { 
        /// <summary>
        /// 饮料
        /// </summary>
        public IEnumerable<MerchantProductDto> Drinks { get; set; }

        /// <summary>
        /// 休闲零食
        /// </summary>
        public IEnumerable<MerchantProductDto> Snacks { get; set; }

        /// <summary>
        /// 香烟
        /// </summary>
        public IEnumerable<MerchantProductDto> Cigarettes { get; set; }

    }
}
