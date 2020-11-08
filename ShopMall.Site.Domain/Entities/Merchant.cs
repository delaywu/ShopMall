namespace ShopMall.Site.Domain.Entities
{
    /// <summary>
    /// 商户信息
    /// </summary>
    public class Merchant : EntityBase<int>
    {
        /// <summary>
        /// 获取或设置 商户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 联系电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 获取或设置 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 获取或设置 店铺评分
        /// </summary>
        public int Score { get; set; }
    }
}
