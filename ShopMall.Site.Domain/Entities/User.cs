using System;

namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User : EntityBase<int>
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 头像 地址
        /// </summary>
        public string Portrait { get; set; }
    }
}
