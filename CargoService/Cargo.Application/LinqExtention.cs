using Cargo.Infrastructure.Data.Model.Settings.CommPayloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cargo.Application
{
    internal static class LinqExtention
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                          bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IEnumerable<T> MyWhereisNullParam<T>(this IEnumerable<T> source, Func<T, bool> predicat, params object[] isNullparam)
        {
            foreach (var item in isNullparam)
            {
                if (item == null || item.Equals(""))
                {
                    return source;
                }
            }

            //process each item individually
            List<T> returnValue = new List<T>();
            foreach (var t in source)
            {
                //string parsedString = stringToParse.ToUpper();

                if (predicat(t))
                    returnValue.Add(t);
            }

            return returnValue;
        }

        public static IEnumerable<T> WithoutShr<T>(this IEnumerable<T> source, Func<T, object> predicatShrSource, string shr)
        {
            if (string.IsNullOrEmpty(shr))
                return source;

            List<T> returnValue = new List<T>();
            char[] separators = new char[] { '/', '\\', ',', ' ' };
            foreach (var t in source)
            {
                var a = predicatShrSource(t);
                var res = a.ToString();
                if (Without(shr, res, separators))
                {
                    returnValue.Add(t);
                }
            }

            return returnValue;
        }

        public static bool Without(string strFind, string strWithout, char[] separators)
        {
            var a = strWithout.Split(separators);
            bool contains = false;
            foreach (var item in a)
            {
                contains = strFind.ToLower().Contains(item.ToLower());
                if (contains)
                    return !contains;
            }

            return !contains;
        }

        #region CommPayloads
        public static IEnumerable<CommPayloadNode> Descendants(this CommPayloadNode root)
        {
            var nodes = new Stack<CommPayloadNode>(new[] { root });
            while (nodes.Any())
            {
                CommPayloadNode node = nodes.Pop();
                //  if (node.Childs==null || !node.Childs.Any())
                {
                    yield return node;
                }

                foreach (var n in node.Childs) nodes.Push(n);
            }
        }

        internal static IEnumerable<CommPayloadNode> Ancestors(this CommPayloadNode node)
        {
            yield return node;

            CommPayloadNode parent = node.Parent;
            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }
        #endregion
    }

}
