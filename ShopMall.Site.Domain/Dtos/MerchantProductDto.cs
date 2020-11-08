using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Dtos
{
    public class MerchantProductDto
    {
        /// <summary>
        /// 获取或设置 商户 外键
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 获取或设置 商品 外键
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 获取 或 设置 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 获取或设置 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 获取或设置 月售
        /// </summary>
        public int Sales { get; set; }
    }
}
