using System;
using System.Linq;
using System.Linq.Expressions;

namespace NEFBDAACommons.Shared.Helpers
{
    public enum ComparisionType
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }
    public static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, object value)
        {
            var parameter = Expression.Parameter(typeof(T));
            var left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);
            var body = MakeComparison(left, comparison, value);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
        public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, ComparisionType comparison, object value)
        {
            return BuildPredicate<T>(propertyName, ComparisonToString(comparison), value);
        }
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static dynamic CreateExpression(Type type, string property)
        {
            var parameter = Expression.Parameter(type, "x");
            Expression body = parameter;
            property.Split('.').ToList().ForEach(member => body = Expression.PropertyOrField(body, member));
            return Expression.Lambda(body, parameter);
        }
        static Expression MakeComparison(Expression left, string comparison, object value)
        {
            var constant = Expression.Constant(value, left.Type);
            switch (comparison)
            {
                case "==":
                    return Expression.MakeBinary(ExpressionType.Equal, left, constant);
                case "!=":
                    return Expression.MakeBinary(ExpressionType.NotEqual, left, constant);
                case ">":
                    return Expression.MakeBinary(ExpressionType.GreaterThan, left, constant);
                case ">=":
                    return Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, left, constant);
                case "<":
                    return Expression.MakeBinary(ExpressionType.LessThan, left, constant);
                case "<=":
                    return Expression.MakeBinary(ExpressionType.LessThanOrEqual, left, constant);
                case "Contains":
                case "StartsWith":
                case "EndsWith":
                    if (value is string)
                    {
                        return Expression.Call(left, comparison, Type.EmptyTypes, constant);
                    }
                    throw new NotSupportedException($"Comparison operator '{comparison}' only supported on string.");
                default:
                    throw new NotSupportedException($"Invalid comparison operator '{comparison}'.");
            }
        }
        private static string ComparisonToString(ComparisionType comparison)
        {
            switch (comparison)
            {
                case ComparisionType.Equal:
                    return "==";
                case ComparisionType.NotEqual:
                    return "!=";
                case ComparisionType.GreaterThan:
                    return ">";
                case ComparisionType.GreaterThanOrEqual:
                    return ">=";
                case ComparisionType.LessThan:
                    return "<";
                case ComparisionType.LessThanOrEqual:
                    return "<=";
                case ComparisionType.Contains:
                    return "Contains";
                case ComparisionType.StartsWith:
                    return "StartsWith";
                case ComparisionType.EndsWith:
                    return "EndsWith";
            }
            throw new NotSupportedException($"Comparison operator {comparison} not supported");
        }
    }

}
