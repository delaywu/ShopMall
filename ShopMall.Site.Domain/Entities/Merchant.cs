using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 商户信息
    /// </summary>
    public class Merchant : EntityBase<Guid>
    {
        /// <summary>
        /// 商户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 店铺评分
        /// </summary>
        public int Score { get; set; }
    }
}
