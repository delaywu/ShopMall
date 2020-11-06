using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ShopMall.Site.Infrastructure.DapperExtensions
{
    internal abstract class SqlQueryUtils
    {
        public static string GetParmName(string propertyName)
        {
            string paramName = propertyName.Replace("[", "").Replace("]", "");
            if (paramName.Contains("."))
            {
                int lstIdx = paramName.LastIndexOf('.');
                paramName = propertyName.Substring(lstIdx + 1).Trim();
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(paramName, "^[A-Za-z0-9_-]*$"))
            {
                return string.Empty;
            }
            return paramName;
        }
      
        /// <summary>
        /// 获取属性名称
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName(LambdaExpression expression)
        {
            Expression expressionToCheck = expression;
            var tokens = new List<string>();

            bool done = false;
            bool accessedMember = false;

            while (!done)
            {
                switch (expressionToCheck.NodeType)
                {
                    case ExpressionType.Convert:

                        accessedMember = false;
                        expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;

                        break;
                    case ExpressionType.ArrayIndex:
                        var binaryExpression = (BinaryExpression)expressionToCheck;

                        Expression indexExpression = binaryExpression.Right;
                        Delegate indexAction = Expression.Lambda(indexExpression).Compile();
                        var value = (int)indexAction.DynamicInvoke();

                        if (accessedMember)
                        {
                            tokens.Add(".");
                        }

                        tokens.Add(string.Format("[{0}]", value));

                        accessedMember = false;
                        expressionToCheck = binaryExpression.Left;

                        break;
                    case ExpressionType.Lambda:
                        var lambdaExpression = (LambdaExpression)expressionToCheck;
                        accessedMember = false;
                        expressionToCheck = lambdaExpression.Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = (MemberExpression)expressionToCheck;

                        if (accessedMember)
                        {
                            tokens.Add(".");
                        }

                        tokens.Add(memberExpression.Member.Name);

                        if (memberExpression.Expression == null)
                        {
                            done = true;
                        }
                        else
                        {
                            accessedMember = true;
                            expressionToCheck = memberExpression.Expression;
                        }
                        break;
                    default:
                        done = true;
                        break;
                }
            }

            tokens.Reverse();

            string result = string.Join(string.Empty, tokens.ToArray());

            return result;
        }
    }

    public enum QueryOper
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description(" = ")]
        Eq,
        /// <summary>
        /// 不等于
        /// </summary> 
        [Description(" <> ")]
        NotEq,
        /// <summary>
        /// 大于
        /// </summary>
        [Description(" > ")]
        Gt,
        /// <summary>
        /// 小于
        /// </summary>
        [Description(" < ")]
        Lt,
        /// <summary>
        /// 大于或等于
        /// </summary>
        [Description(" >= ")]
        Ge,
        /// <summary>
        /// 小于或等于
        /// </summary>
        [Description(" <= ")]
        Le,
        /// <summary>
        /// 左右模糊匹配
        /// </summary>
        [Description(" LIKE ")]
        Like,
        /// <summary>
        /// 为空
        /// </summary>
        [Description(" IS NULL ")]
        IsNull,
        /// <summary>
        /// 不为空
        /// </summary>
        [Description(" IS NOT NULL ")]
        IsNotNull,
        /// <summary>
        /// 区间
        /// </summary>
        [Description(" BETWEEN ")]
        Between
    }
    /// <summary>
    /// Specifies the Sort order for the Grid.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending Order.
        /// </summary> 
        Ascending = 0,

        /// <summary>
        /// Descending Order.
        /// </summary> 
        Descending
    }
}
