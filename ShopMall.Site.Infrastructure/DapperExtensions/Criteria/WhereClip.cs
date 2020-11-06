using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    public class WhereClip : ICriteria
    {
        protected StringBuilder sbWhereSql = new StringBuilder();
        protected bool toWrapBrackets = false;
        protected readonly List<DataParameter> parameters = new List<DataParameter>();
        public string WhereSql
        {
            get
            {
                return sbWhereSql.ToString();
            }
            set
            {
                sbWhereSql = new StringBuilder(value);
            }
        }
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
                fixWhere = value;
            }
        }
        public string ToSql()
        {
            StringBuilder sbSql = new StringBuilder();
            if (sbWhereSql.Length>0)
            {
                if (fixWhere)
                {
                    sbSql.Append(" WHERE ");
                }
                else
                {
                    sbSql.Append(" AND ");
                }
                sbSql.Append(WhereSql);
            }
            return sbSql.ToString();
        }
        internal List<DataParameter> Parameters
        {
            get
            {
                return parameters;
            }
        }
        public static bool IsNullOrEmpty(WhereClip where)
        {
            return ((object)where) == null || where.sbWhereSql.Length == 0;
        }
        /// <summary>
        /// 组成dapper动态参数
        /// </summary>
        /// <returns></returns>
        public DynamicParameters GetDynamicParms()
        {
            DynamicParameters dynamicParms = null;
            if (this.parameters != null && this.parameters.Count > 0)
            {
                dynamicParms = new DynamicParameters();
                List<DataParameter>.Enumerator en = this.parameters.GetEnumerator();
                while (en.MoveNext())
                {
                    dynamicParms.Add(en.Current.Name, en.Current.Value == null ? DBNull.Value : en.Current.Value);
                }
            }
            return dynamicParms;
        }
        /// <summary>
        ///  AND
        /// </summary>
        /// <param name="whereClip"></param>
        /// <returns></returns>
        public WhereClip And(WhereClip whereClip)
        {
            if (IsNullOrEmpty(whereClip))
            {
                return this;
            }
            AddParameters(whereClip);
            if (sbWhereSql.Length > 0)
            {
                sbWhereSql.Append(" AND ");
                if (whereClip.toWrapBrackets)
                {
                    sbWhereSql.Append($"({whereClip.WhereSql})");
                }
                else
                {
                    sbWhereSql.Append($"{whereClip.WhereSql}");
                }
            }
            else
            {
                sbWhereSql.Append(whereClip.WhereSql);
            }
            this.toWrapBrackets = true;
            return this;
        }
        /// <summary>
        /// OR 
        /// </summary>
        /// <param name="whereClip"></param>
        /// <returns></returns>
        public WhereClip Or(WhereClip whereClip)
        {
            if (IsNullOrEmpty(whereClip))
            {
                return this;
            }
            AddParameters(whereClip);
            if (sbWhereSql.Length > 0)
            {
                sbWhereSql.Append(" OR ");
                if (whereClip.toWrapBrackets)
                {
                    sbWhereSql.Append($"({whereClip.WhereSql})");
                }
                else
                {
                    sbWhereSql.Append($"{whereClip.WhereSql}");
                }
            }
            else
            {
                sbWhereSql.Append(whereClip.WhereSql);
            }
            this.toWrapBrackets = true;
            return this;
        }
        /// <summary>
        /// 非
        /// </summary>
        /// <returns></returns>
        public WhereClip Not()
        {
            if (IsNullOrEmpty(this))
            {
                return this;
            }
            sbWhereSql.Insert(0, "NOT(");
            sbWhereSql.Append(") ");
            this.toWrapBrackets = false;
            return this;
        }
        /// <summary>
        /// 括起来
        /// </summary>
        /// <returns></returns>
        public WhereClip ToWrap()
        {
            if (IsNullOrEmpty(this))
            {
                return this;
            }
            sbWhereSql.Insert(0, "(");
            sbWhereSql.Append(") ");
            this.toWrapBrackets = false;
            return this;
        }
        public WhereClip ApendWhere(string strSubWhere)
        {
            sbWhereSql.Append(strSubWhere);
            return this;
        }
        public static WhereClip operator &(WhereClip left, WhereClip right)
        {
            WhereClip newWhere = new WhereClip();
            newWhere.And(left);
            newWhere.And(right);
            return newWhere;
        }

        public static WhereClip operator |(WhereClip left, WhereClip right)
        {
            WhereClip newWhere = new WhereClip();
            newWhere.Or(left);
            newWhere.Or(right);
            return newWhere;
        }

        public static WhereClip operator !(WhereClip right)
        {
            return right.Not();
        }
        private void AddParameters(WhereClip whereClip)
        {
            if (whereClip == null || whereClip.Parameters == null)
            {
                return;
            }
            List<DataParameter> additional = whereClip.Parameters;
            foreach (DataParameter item in additional)
            {
                string propertyName = item.Name;
                int chkFix = item.Name.IndexOf("_pfix_");
                if (chkFix > 0)
                {
                    propertyName = item.Name.Substring(0, chkFix);
                }
                DataParameter targetItem = parameters.FirstOrDefault(m => m.Name == item.Name);
                if (targetItem == null)
                {
                    parameters.Add(item);
                }
                else
                {
                    if ((item.Value == null && targetItem.Value == null) || (item.Value != null && targetItem.Value != null && item.Value.Equals(targetItem.Value)))
                    {
                        continue;
                    }
                    string srcParmName = item.Name;
                    int count = parameters.Count(m => m.Name.StartsWith($"{srcParmName}_pfix_"));
                    string newParmName = $"{srcParmName}_pfix_{count + 1}";
                    item.Name = newParmName;
                    parameters.Add(item);
                    bool chk = additional.Any(m => m.Name == newParmName);
                    string replace = chk ? $"@{newParmName}_pfixtmp " : $"@{newParmName} ";
                    whereClip.WhereSql = whereClip.WhereSql.Replace($"@{srcParmName} ", replace);
                }
            }
            whereClip.WhereSql = whereClip.WhereSql.Replace("_pfixtmp", "");
        }
    }
}
