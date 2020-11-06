using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    /// <summary>
    /// 排序
    /// </summary>
    public class OrderByClip
    {
        List<KeyValuePair<string, SortDirection>> orderBys = new List<KeyValuePair<string, SortDirection>>();
        internal OrderByClip()
        {
        }
        internal OrderByClip OrderBy(string propertyName, SortDirection sortDirection = SortDirection.Ascending)
        {
            orderBys.Add(new KeyValuePair<string, SortDirection>(propertyName, sortDirection));
            return this;
        }
        internal OrderByClip OrderBy<TEntity>(Expression<Func<TEntity, object>> expression, SortDirection sortDirection = SortDirection.Ascending)
        {
            string propertyName = SqlQueryUtils.GetPropertyName(expression);
            return OrderBy(propertyName, sortDirection);
        }
        internal string ToOrderBySql()
        {
            if (orderBys.Any())
            {
                StringBuilder sb = new StringBuilder(" order by ");
                List<KeyValuePair<string, SortDirection>>.Enumerator en = orderBys.GetEnumerator();
                int idx = 0;
                while (en.MoveNext())
                {
                    string sortDirectionString = en.Current.Value == SortDirection.Descending ? " DESC" : "";
                    if (idx == 0)
                    {
                        sb.Append($"{en.Current.Key}{sortDirectionString}");
                    }
                    else
                    {
                        sb.Append($",{en.Current.Key}{sortDirectionString}");
                    }
                    idx++;
                }
                return sb.ToString();
            }
            return string.Empty;
        }
    }
}
