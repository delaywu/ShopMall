using System;

namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 商品类别
    /// </summary>
    public class Category : EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 商品类别名称
        /// </summary>
        public string Name { get; set; } 
    }
}
