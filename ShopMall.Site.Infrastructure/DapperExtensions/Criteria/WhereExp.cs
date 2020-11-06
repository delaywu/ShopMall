using Dapper;
using ShopMall.Site.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public static class Oper
    {
        #region 字符串表达式
        /// <summary>
        /// 等于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip Eq(string propertyName, object value, string paramName = null)
        {
            return BuildWhereChip(propertyName, value, QueryOper.Eq, paramName);
        }
        /// <summary>
        /// 不等于
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip NotEq(string propertyName, object value, string paramName = null)
        {
            return BuildWhereChip(propertyName, value, QueryOper.NotEq, paramName);
        }
        /// <summary>
        /// 为空
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static WhereClip IsNull(string propertyName)
        {
            return BuildWhereChip(propertyName, null, QueryOper.IsNull, null);
        }
        /// <summary>
        /// 不为空
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static WhereClip IsNotNull(string propertyName)
        {
            return BuildWhereChip(propertyName, null, QueryOper.IsNotNull, null);
        }
        /// <summary>
        /// 左右模糊匹配
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip Like(string propertyName, string value, string paramName = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return BuildWhereChip(propertyName, value, QueryOper.Like, paramName);
        }
        /// <summary>
        /// Apply a "like" constraint to the named property auto add [%] on the prefix.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip StartWith(string propertyName, string value, string paramName = null)
        {
            return Like(propertyName, value.Replace("%", "[%]").Replace("_", "[_]") + '%', paramName);
        }
        /// <summary>
        /// Apply a "like" constraint to the named property, auto add % on the end
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        /// <returns>A <see cref="WhereClip" />.</returns>
        public static WhereClip EndsWith(string propertyName, string value, string paramName = null)
        {
            return Like(propertyName, '%' + value.Replace("%", "[%]").Replace("_", "[_]"), paramName);
        }
        /// <summary>
        /// Apply a "greater than" constraint to the named property
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Gt(string propertyName, object value, string paramName = null)
        {
            return BuildWhereChip(propertyName, value, QueryOper.Gt, paramName);
        }

        /// <summary>
        /// Apply a "less than" constraint to the named property
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Lt(string propertyName, object value, string paramName = null)
        {
            return BuildWhereChip(propertyName, value, QueryOper.Lt, paramName);
        }

        /// <summary>
        /// Apply a "less than or equal" constraint to the named property
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Le(string propertyName, object value, string paramName = null)
        {
            return BuildWhereChip(propertyName, value, QueryOper.Le, paramName);
        }

        /// <summary>
        /// Apply a "greater than or equal" constraint to the named property
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Ge(string propertyName, object value, string paramName = null)
        {
            return BuildWhereChip(propertyName, value, QueryOper.Ge, paramName);
        }
        /// <summary>
        /// Apply a "between" constraint to the named property
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="lo">The low value for the Property.</param>
        /// <param name="hi">The high value for the Property.</param>
        /// <returns>A <see cref="WhereClip" />.</returns>
        public static WhereClip Between(string propertyName, object lo, object hi, string paramName = null)
        {
            WhereClip where = new WhereClip();
            StringBuilder sbSql = new StringBuilder($"{propertyName} between ");
            if (paramName == null)
            {
                paramName = SqlQueryUtils.GetParmName(propertyName);
            }
            if (paramName.Length > 0)
            {
                string strParamName1 = paramName + "_pmin";
                string strParamName2 = paramName + "_pmax";
                sbSql.Append($"@{strParamName1} and @{strParamName2} ");
                where.Parameters.Add(new DataParameter(strParamName1, lo));
                where.Parameters.Add(new DataParameter(strParamName2, hi));
            }
            else
            {
                sbSql.Append($"{lo} and {hi} ");
            }
            where.WhereSql = sbSql.ToString();
            return where;
        }
        /// <summary>
        /// In  (Dapper.Net)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip DapperIn<T>(string propertyName, IEnumerable<T> values, string paramName = null)
        {
            if (values == null || !values.Any())
            {
                return null;
            }
            WhereClip where = new WhereClip();
            if (paramName == null)
            {
                paramName = SqlQueryUtils.GetParmName(propertyName);
            }
            if (paramName.Length > 0)
            {
                where.Parameters.Add(new DataParameter(paramName, values));
                where.WhereSql = $"{propertyName} in @{paramName} ";
            }
            else
            {
                if (typeof(T).FullName == typeof(string).FullName)
                {
                    string strIn = string.Join<T>("','", values);
                    where.WhereSql = $"{propertyName} in ('{strIn}') ";
                }
                else
                {
                    string strIn = string.Join<T>(",", values);
                    where.WhereSql = $"{propertyName} in ({strIn}) ";
                }
            }
            return where;
        }
        /// <summary>
        /// Not In  (Dapper.Net)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip DapperNotIn<T>(string propertyName, IEnumerable<T> values, string paramName = null)
        {
            if (values == null || !values.Any())
            {
                return null;
            }
            WhereClip where = new WhereClip();
            if (paramName == null)
            {
                paramName = SqlQueryUtils.GetParmName(propertyName);
            }
            if (paramName.Length > 0)
            {
                where.Parameters.Add(new DataParameter(paramName, values));
                where.WhereSql = $"{propertyName} not in @{paramName} ";
            }
            else
            {
                if (typeof(T).FullName == typeof(string).FullName)
                {
                    string strIn = string.Join<T>("','", values);
                    where.WhereSql = $"{propertyName} not in ('{strIn}') ";
                }
                else
                {
                    string strIn = string.Join<T>(",", values);
                    where.WhereSql = $"{propertyName} not in ({strIn}) ";
                }
            }
            return where;
        }
        #endregion

        #region Lambda表达式
        /// <summary>
        /// 等于
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip Eq<TEntity>(Expression<Func<TEntity, object>> expression, object value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Eq(propertyName, value, paramName);
        }
        /// <summary>
        /// 不等于
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip NotEq<TEntity>(Expression<Func<TEntity, object>> expression, object value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return NotEq(propertyName, value, paramName);
        }
        /// <summary>
        /// 为空
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static WhereClip IsNull<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return IsNull(propertyName);
        }
        /// <summary>
        /// 不为空
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static WhereClip IsNotNull<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return IsNotNull(propertyName);
        }
        /// <summary>
        /// 左右模糊匹配
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip Like<TEntity>(Expression<Func<TEntity, object>> expression, string value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Like(propertyName, value, paramName);
        }
        /// <summary>
        /// Apply a "like" constraint to the named property auto add [%] on the prefix.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip StartWith<TEntity>(Expression<Func<TEntity, object>> expression, string value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return StartWith(propertyName, value, paramName);
        }
        /// <summary>
        /// Apply a "like" constraint to the named property, auto add % on the end
        /// </summary>
        /// <param name="expression">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        /// <returns>A <see cref="WhereClip" />.</returns>
        public static WhereClip EndsWith<TEntity>(Expression<Func<TEntity, object>> expression, string value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return EndsWith(propertyName, value, paramName);
        }
        /// <summary>
        /// Apply a "greater than" constraint to the named property
        /// </summary>
        /// <param name="expression">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Gt<TEntity>(Expression<Func<TEntity, object>> expression, object value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Gt(propertyName, value, paramName);
        }

        /// <summary>
        /// Apply a "less than" constraint to the named property
        /// </summary>
        /// <param name="expression">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Lt<TEntity>(Expression<Func<TEntity, object>> expression, object value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Lt(propertyName, value, paramName);
        }

        /// <summary>
        /// Apply a "less than or equal" constraint to the named property
        /// </summary>
        /// <param name="expression">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Le<TEntity>(Expression<Func<TEntity, object>> expression, object value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Le(propertyName, value, paramName);
        }

        /// <summary>
        /// Apply a "greater than or equal" constraint to the named property
        /// </summary>
        /// <param name="expression">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        public static WhereClip Ge<TEntity>(Expression<Func<TEntity, object>> expression, object value, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Ge(propertyName, value, paramName);
        }
        /// <summary>
        /// Apply a "between" constraint to the named property
        /// </summary>
        /// <param name="expression">The name of the Property in the class.</param>
        /// <param name="lo">The low value for the Property.</param>
        /// <param name="hi">The high value for the Property.</param>
        /// <returns>A <see cref="WhereClip" />.</returns>
        public static WhereClip Between<TEntity>(Expression<Func<TEntity, object>> expression, object lo, object hi, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return Between(propertyName, lo, hi, paramName);
        }
        /// <summary>
        /// Apply an "in" constraint to the named property (Dapper.Net)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="values"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip DapperIn<TEntity, T>(Expression<Func<TEntity, object>> expression, IEnumerable<T> values, string paramName = null) where TEntity : class
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return DapperIn(propertyName, values, paramName);
        }
        /// <summary>
        /// Not IN  (Dapper.Net)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="values"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static WhereClip DapperNotIn<TEntity, T>(Expression<Func<TEntity, object>> expression, IEnumerable<T> values, string paramName = null) where TEntity : class
        {
            return DapperNotIn<TEntity, T>(expression, values, paramName);
        }
        #endregion

        /// <summary>
        /// 构造查询条件
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static WhereClip BuildWhereChip(string propertyName, object value, QueryOper op, string paramName = null)
        {
            if (op != QueryOper.IsNull && op != QueryOper.IsNotNull && (value == null || value == DBNull.Value))
            {
                return null;
            }
            WhereClip where = new WhereClip();
            StringBuilder sbSql = new StringBuilder($"{propertyName}{op.ToDescription()}");
            if (value != null && value != DBNull.Value)
            {
                if (paramName == null)
                {
                    paramName = SqlQueryUtils.GetParmName(propertyName);
                }
                if (paramName.Length > 0)
                {
                    sbSql.Append($"@{paramName} ");
                    where.Parameters.Add(new DataParameter(paramName, value));
                }
                else
                {
                    sbSql.Append($"{value} ");
                }
            }
            where.WhereSql = sbSql.ToString();
            return where;
        }
        /// <summary>
        /// 获取条件的sql语句(对象为空时返回空字符串)
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public static string GetSafeSql(this ICriteria criteria)
        {
            if (criteria != null)
            {
                return criteria.ToSql();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 组装sql
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="selectPreSql"></param>
        /// <returns></returns>
        public static string FormatSql(this ICriteria criteria, string selectPreSql)
        {
            selectPreSql = selectPreSql ?? "";
            if (criteria != null)
            {
                selectPreSql.Replace(")group", ") group");
                string criteriaSql = criteria.ToSql();
                criteriaSql = criteriaSql.Replace("  ", " ");
                int lstBracket = selectPreSql.LastIndexOf('(');
                int lstGroup = selectPreSql.LastIndexOf(" group ",StringComparison.OrdinalIgnoreCase);
                if (lstGroup > 0 && lstGroup > lstBracket)
                {
                    int idxOrderBy = criteriaSql.LastIndexOf(" order ", StringComparison.OrdinalIgnoreCase);
                    if (idxOrderBy == 0) //无where条件
                    {
                        return selectPreSql + criteriaSql;
                    }
                    else if (idxOrderBy == -1) //无排序
                    {
                        StringBuilder sbSql = new StringBuilder();
                        sbSql.Append(selectPreSql.Substring(0, lstGroup));
                        sbSql.Append(" ");
                        sbSql.Append(criteriaSql);
                        sbSql.Append(" ");
                        sbSql.Append(selectPreSql.Substring(lstGroup));
                        return sbSql.ToString();
                    }
                    else
                    {
                        StringBuilder sbSql = new StringBuilder();
                        sbSql.Append(selectPreSql.Substring(0, lstGroup));
                        sbSql.Append(" ");
                        sbSql.Append(criteriaSql.Substring(0, idxOrderBy));
                        sbSql.Append(" ");
                        sbSql.Append(selectPreSql.Substring(lstGroup));
                        sbSql.Append(" ");
                        sbSql.Append(criteriaSql.Substring(idxOrderBy));
                        return sbSql.ToString();
                    }
                }
                return selectPreSql + criteria.ToSql();
            }
            else
            {
                return selectPreSql;
            }
        }
        /// <summary>
        /// 获取条件的参数(对象为空时返回空)
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public static DynamicParameters GetSafeParameters(this ICriteria criteria)
        {
            if (criteria != null)
            {
                return criteria.GetDynamicParms();
            }
            else
            {
                return null;
            }
        }

    }
}
