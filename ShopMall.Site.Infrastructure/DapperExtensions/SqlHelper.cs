using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public static  class SqlPageHelper
    {
        /// <summary>
        /// 获取统计记录sql语句
        /// </summary>
        /// <param name="selectSql"></param>
        /// <returns></returns>
        public static string GetCountSql(string selectSql)
        {
            string countSql = string.Empty;
            int fromIdx = selectSql.IndexOf(" from ", StringComparison.OrdinalIgnoreCase);
            int subLen = selectSql.Length - fromIdx;
            int orderIdx = selectSql.LastIndexOf(" order by", StringComparison.OrdinalIgnoreCase);
            if (orderIdx > 0)
            {
                subLen = orderIdx - fromIdx;
            }
            int idx1 = selectSql.IndexOf(" distinct ", StringComparison.OrdinalIgnoreCase);
            int idx2 = selectSql.IndexOf(" group by ", StringComparison.OrdinalIgnoreCase);
            if ((idx1 > 0 && idx1 < fromIdx))
            {
                countSql = "select count(1) from (" + selectSql.Substring(0, orderIdx) + ") tc";
            }
            else if (idx2 > 0)
            {
                countSql = "select count(1) from (select 1 as rv " + selectSql.Substring(fromIdx, subLen) + ") tc";
            }
            else
            {
                countSql = "select count(1) " + selectSql.Substring(fromIdx, subLen);
            }
            return countSql;
        }
        /// <summary>
        /// 获取分页语句(必须包含排序语句)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="selectSql">查询语句，必须包含排序语句</param>
        /// <returns></returns>
        public static string GetPagingSql(int pageIndex, int pageSize, string selectSql)
        {
            selectSql = selectSql.Trim();
            string strPageSql = string.Empty;
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 25;
            }
            if (pageIndex == 1)
            {
                string subStr = selectSql.Trim().Substring(7);//去掉开始的select
                if (subStr.Trim().ToLower().StartsWith("distinct "))
                {
                    strPageSql = string.Format("select distinct top {0} {1}", pageSize, subStr.Substring(9));
                }
                else
                {
                    strPageSql = string.Format("select top {0} {1}", pageSize, subStr);
                }
                return strPageSql;
            }
            int orderIdx = selectSql.ToLower().LastIndexOf(" order by");
            string orderBy = "order by getdate()";
            int fromIdx = selectSql.ToLower().IndexOf(" from ");
            if (orderIdx >= 0)
            {
                orderBy = selectSql.Substring(orderIdx).Trim();
            }
            else
            {
                orderIdx = selectSql.Length;
            }
            StringBuilder sbPre = new StringBuilder();
            sbPre.Append(selectSql.Substring(0, fromIdx));
            sbPre.AppendFormat(",ROW_NUMBER() OVER({0}) AS RankNumber", orderBy);
            sbPre.Append(selectSql.Substring(fromIdx, orderIdx - fromIdx));
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("select * from ({0}) as t_0 ", sbPre.ToString());
            sbSql.AppendFormat(" where rankNumber BETWEEN {0} and {1}", (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            strPageSql = sbSql.ToString();
            return strPageSql;
        }
        /// <summary>
        /// 获取分页数据(自定义sql语句)
        /// </summary>
        /// <typeparam name="TReturn">结果集数据类型/实体</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="selectSql">查询语句</param>
        /// <param name="parms">参数</param>
        /// <param name="countSql">统计记录数语句，不传则自动根据查询语句生成</param>
        /// <returns></returns>
        public static PagerData<TReturn> GetPagerData<TReturn>(this IDbConnection conn, int pageIndex, int pageSize, string selectSql, object parms = null, string countSql = null)
        {
            PagerData<TReturn> res = new PagerData<TReturn>(pageIndex, pageSize);
            if (string.IsNullOrEmpty(countSql))
            {
                countSql = GetCountSql(selectSql);
            }
            res.RecordCount = conn.ExecuteScalar<int>(countSql, parms);
            if (res.RecordCount > 0)
            {
                if (pageIndex > res.PageCount)
                {
                    pageIndex = res.PageCount;
                    res.PageIndex = res.PageCount;
                }
                string strPagerSql = GetPagingSql(res.PageIndex, res.PageSize, selectSql);
                res.Data = conn.Query<TReturn>(strPagerSql, parms);
            }
            else
            {
                pageIndex = 1;
                res.PageIndex = 1;
                res.Data = new List<TReturn>();
            }
            return res;
        }
        /// <summary>
        /// 异步获取分页数据(自定义sql语句)
        /// </summary>
        /// <typeparam name="TReturn">结果集数据类型/实体</typeparam>
        /// <param name="conn">数据库连接</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="selectSql">查询语句</param>
        /// <param name="parms">参数</param>
        /// <param name="countSql">统计记录数语句，不传则自动根据查询语句生成</param>
        /// <returns></returns>
        public static async Task<PagerData<TReturn>> GetPagerDataAsync<TReturn>(this IDbConnection conn, int pageIndex, int pageSize, string selectSql, object parms = null, string countSql = null)
        {
            PagerData<TReturn> res = new PagerData<TReturn>(pageIndex, pageSize);
            if (string.IsNullOrEmpty(countSql))
            {
                countSql = GetCountSql(selectSql);
            }
            res.RecordCount = await conn.ExecuteScalarAsync<int>(countSql, parms);
            if (res.RecordCount > 0)
            {
                if (pageIndex > res.PageCount)
                {
                    pageIndex = res.PageCount;
                    res.PageIndex = res.PageCount;
                }
                string strPagerSql = GetPagingSql(res.PageIndex, res.PageSize, selectSql);
                try
                {

                    res.Data = await conn.QueryAsync<TReturn>(strPagerSql, parms);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                pageIndex = 1;
                res.PageIndex = 1;
                res.Data = new List<TReturn>();
            }
            return res;
        }
        /// <summary>
        /// 同步获取分页数据
        /// </summary>
        /// <typeparam name="TReturn">结果集数据类型/实体</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="selectPreSql">查询语句(前部分，不带参数)</param>
        /// <param name="criteria">附加条件</param>
        /// <returns></returns>
        public static PagerData<TReturn> GetPagerData<TReturn>(this IDbConnection connection, int pageIndex, int pageSize, string selectPreSql, ICriteria criteria)
        {
            string strSql = criteria.FormatSql(selectPreSql);
            return connection.GetPagerData<TReturn>(pageIndex, pageSize, strSql, criteria.GetSafeParameters());
        }
        /// <summary>
        /// 异步获取分页数据
        /// </summary>
        /// <typeparam name="TReturn">结果集数据类型/实体</typeparam>
        /// <param name="connection">数据库连接</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="selectPreSql">查询语句(前部分，不带参数)</param>
        /// <param name="criteria">附加条件</param>
        /// <returns></returns>
        public static async Task<PagerData<TReturn>> GetPagerDataAsync<TReturn>(this IDbConnection connection, int pageIndex, int pageSize, string selectPreSql, ICriteria criteria)
        {
            string strSql = criteria.FormatSql(selectPreSql);
            return await connection.GetPagerDataAsync<TReturn>(pageIndex, pageSize, strSql, criteria.GetSafeParameters());
        }

        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="connection"></param>
        /// <param name="selectPreSql"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TReturn>> GetListAsync<TReturn>(this IDbConnection connection, string selectPreSql, ICriteria criteria)
        {
            string strSql = criteria.FormatSql(selectPreSql);
            return await connection.QueryAsync<TReturn>(strSql, criteria.GetSafeParameters());
        }
    }
}
