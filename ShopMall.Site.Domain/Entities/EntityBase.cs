using ShopMall.Site.Infrastructure.Entity;
using System;
using System.ComponentModel;

namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey> : IEntity<TKey> 
    {
        /// <summary>
        /// 获取或设置 主键
        /// </summary>
        [DisplayName("主键")]
        public TKey Id { get; set; }

        /// <summary>
        /// 获取或设置 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 获取或设置 修改日期
        /// </summary>
        public DateTime? ModifyTime { get; set; }  
    }
}
