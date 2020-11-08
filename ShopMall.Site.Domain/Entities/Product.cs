using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Entities
{
    public class Product : EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 图片链接
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 获取或设置 推荐指数
        /// </summary>
        public int Recommend { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品分类 外键
        /// </summary>
        public int CategoryId { get; set; }
    }
}
