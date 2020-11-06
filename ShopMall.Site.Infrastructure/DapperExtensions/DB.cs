using Microsoft.Extensions.Configuration;
using ShopMall.Site.Infrastructure.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public class DB
    {
        public static readonly string readstr = ConfigurationManager.Configuration.GetConnectionString("Read");
        public static readonly string writestr = ConfigurationManager.Configuration.GetConnectionString("Write");

        /// <summary>
        /// 获取 读数据库 连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection ConnRead() => new SqlConnection(readstr);

        /// <summary>
        /// 获取 写数据库 连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection ConnWrite() => new SqlConnection(writestr);

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection Conn(string dbstr) => new SqlConnection(dbstr);

        public static List<T> ExecutePage<T>(string StrFields, string StrTablesAndWhere, string StrKeyName, string StrSortOrder, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            RecordCount = 1;
            PageCount = 1;
            List<T> list = new List<T>();
            return list;
        }
    }
}
