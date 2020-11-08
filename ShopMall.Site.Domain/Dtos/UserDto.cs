using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Domain.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

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
