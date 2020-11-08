using System.Collections.Generic;
using System.Threading.Tasks;
using ShopMall.Site.Domain.Models;

namespace ShopMall.Site.Domain.IRepositories
{
    public interface IHomeRepositories : IDependency
    {
        /// <summary>
        /// 获取 首页数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<HomeModel> HomeEntitiesAsync(int merchantId);
    }
}
