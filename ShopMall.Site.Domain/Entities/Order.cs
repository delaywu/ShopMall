using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 用户 外键
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime DeliveriesTime { get; set; }

        /// <summary>
        /// 对应的商户商品外键
        /// </summary>
        public int MerchantProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 订单总价格
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
