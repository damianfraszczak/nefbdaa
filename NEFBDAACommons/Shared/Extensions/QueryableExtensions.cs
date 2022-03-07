using Microsoft.EntityFrameworkCore;
using NEFBDAACommons.Database.Models.PagedList;
using NEFBDAACommons.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NEFBDAACommons.Shared.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByMultiple<T>(this IQueryable<T> queryable, IEnumerable<Order> orders)
        {
            var enumerable = orders as Order[] ?? orders.ToArray();
            if (queryable?.Any() != true || enumerable?.Any() != true)
            {
                return queryable;
            }

            for (var i = 0; i < enumerable.Count(); i++)
            {
                var order = enumerable[i];
                queryable = CreateOrderedQueryable(queryable, order.Property, order.IsAscending, i > 0);
            }

            return queryable;
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> queryable, string property, bool isAscending)
        {
            return CreateOrderedQueryable(queryable, property, isAscending);
        }

        private static IQueryable<T> CreateOrderedQueryable<T>(this IQueryable<T> queryable, string property,
            bool isAscending, bool isNext = false)
        {
            if (queryable?.Any() != true || string.IsNullOrEmpty(property))
            {
                return queryable;
            }

            var expression = ExpressionHelper.CreateExpression(typeof(T), property);
            
            if (isNext && queryable is IOrderedQueryable<T> ts)
            {
                return (isAscending)
                    ? Queryable.ThenBy(ts, expression)
                    : Queryable.ThenByDescending(ts, expression);
            }
            else
            {
                return (isAscending)
                    ? Queryable.OrderBy(queryable, expression)
                    : Queryable.OrderByDescending(queryable, expression);
            }
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> queryable, Order order)
        {
            return queryable?.Order(order?.Property, order?.IsAscending ?? true);
        }

        /// <summary>
        /// Pagination form 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int index, int size)
        {
            if (queryable?.Any() != true || index == 0 || size == 0)
            {
                return queryable;
            }

            return queryable.Skip((index - 1) * size).Take(size);
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> queryable, Expression<Func<T, object>>[] includes)
            where T : class
        {
            includes?.ToList().ForEach(include => queryable = queryable.Include(include));
            return queryable;
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> queryable, PagedListParameters pagedListParameters)
        {
            return new PagedList<T>(queryable, pagedListParameters);
        }

        //https://stackoverflow.com/questions/46306955/how-to-construct-a-dynamic-where-filter-in-ef-core-to-handle-equals-like-gt-l
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string propertyName, string comparison,
            string value)
        {
            return source.Where(ExpressionHelper.BuildPredicate<T>(propertyName, comparison, value));
        }

        public static IQueryable<T> WhereStringContains<T>(this IQueryable<T> query, string propertyName,
            string contains)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] {typeof(string)});
            var someValue = Expression.Constant(contains, typeof(string));
            var containsExpression = Expression.Call(propertyExpression, method, someValue);

            return query.Where(Expression.Lambda<Func<T, bool>>(containsExpression, parameter));
        }
    }
}