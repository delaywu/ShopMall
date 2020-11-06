using System;

namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 商品类别
    /// </summary>
    public class Category : EntityBase<Guid>
    {
        public string Name { get; set; } 
    }
}
