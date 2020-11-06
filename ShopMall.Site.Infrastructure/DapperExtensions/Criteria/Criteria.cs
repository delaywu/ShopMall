using Dapper;
using System;
using System.Linq.Expressions;
using System.Text;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public class Criteria:ICriteria
    {
        private WhereClip _where = new WhereClip();
        private OrderByClip _orderby = new OrderByClip();
        private bool fixWhere = true;
        /// <summary>
        /// sql语句前是否添加where
        /// </summary>
        public bool FixWhere
        {
            get
            {
                return fixWhere;
            }
            set
            {
                _where.FixWhere = value;
                fixWhere = value;
            }
        }
        public string ToSql()
        {
            StringBuilder sbSql = new StringBuilder();
            if (_where.WhereSql.Length > 0)
            {
                if (fixWhere)
                {
                    sbSql.Append(" WHERE ");
                }
                else
                {
                    sbSql.Append(" AND ");
                }
                sbSql.Append(_where.WhereSql);
            }
            string orderbySql = _orderby.ToOrderBySql();
            sbSql.Append(orderbySql);
            return sbSql.ToString();
        }
        public DynamicParameters GetDynamicParms()
        {
            return _where.GetDynamicParms();
        }
        public Criteria And(WhereClip whereClip)
        {
            _where.And(whereClip);
            return this;
        }
        public Criteria Or(WhereClip whereClip)
        {
            _where.Or(whereClip);
            return this;
        }
        public Criteria Not()
        {
            _where.Not();
            return this;
        }
        public Criteria ToWrap()
        {
            _where.ToWrap();
            return this;
        }
        public Criteria OrderBy(string propertyName, SortDirection sortDirection = SortDirection.Ascending)
        {
            this._orderby.OrderBy(propertyName, sortDirection);
            return this;
        }
        public Criteria OrderBy<TEntity>(Expression<Func<TEntity, object>> expression, SortDirection sortDirection = SortDirection.Ascending)
        {
            this._orderby.OrderBy<TEntity>(expression, sortDirection);
            return this;
        }
    }
}
