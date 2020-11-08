using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 用户地址信息管理
    /// </summary>
    public class Address : EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 用户 主键
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置 小区
        /// </summary>
        public string Village { get; set; }

        /// <summary>
        /// 获取或设置 房间号
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// 获取或设置 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 获取或设置 纬度
        /// </summary>
        public double Latitude { get; set; }
    }
}
