using Dapper;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public interface ICriteria
    {
        bool FixWhere { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        /// <param name="fixWhere"></param>
        /// <returns></returns>
        string ToSql();
        /// <summary>
        /// 获取参数集合
        /// </summary>
        /// <returns></returns>
        DynamicParameters GetDynamicParms();
    }
}
