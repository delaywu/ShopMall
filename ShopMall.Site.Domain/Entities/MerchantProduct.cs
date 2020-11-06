using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Entities
{
    public class MerchantProduct : EntityBase<Guid>
    {
        /// <summary>
        /// 商户 外键
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 商品 外键
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 月售
        /// </summary>
        public int Sales { get; set; }
    }
}
