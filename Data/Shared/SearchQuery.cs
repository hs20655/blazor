using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public class SearchQuery
    {
        public DateTime? Timestamp { get; set; }
        public List<SearchCondition>? Conditions { get; set; }
        public bool OrElse { get; set; }

        private readonly List<SearchCondition> emptyConditions = new List<SearchCondition>();

        public IQueryable<T> ToQuery<T>(DbSet<T> set)
            where T : Entity
        {
            if (Conditions == null) return set;

            var entity = Expression.Parameter(typeof(T), "entity");
            var matches = (Conditions ?? emptyConditions).Select(s => Condition<T>(s, entity))
                .Aggregate(default(Expression), (acc, c) =>
                    acc != null ? (OrElse ? Expression.Or(acc, c) : Expression.And(acc, c)) : c ??
                        Expression.Constant(true));
            var bookmark = Bookmark<T>(entity);
            var predicate = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(bookmark, matches), entity);
            return set.Where(predicate);
        }

        private Expression Bookmark<T>(ParameterExpression entity)
            where T : Entity
        {
            if (Timestamp == null)
            {
                return Expression.Constant(true);
            }

            var createdDate = Expression.Property(entity, nameof(Entity.CreatedDate));
            var updatedDate = Expression.Property(entity, nameof(Entity.UpdatedDate));
            var timestamp = Expression.LessThanOrEqual(
                Expression.Coalesce(updatedDate, createdDate),
                Expression.Constant(Timestamp, typeof(DateTime)));
            return timestamp;
        }

        private Expression Condition<T>(
            SearchCondition condition, ParameterExpression entity)
        {
            var field = Expression.Property(entity, condition.Field);
            var propertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            var propertyType = typeof(T).GetProperty(condition.Field, propertyFlags).PropertyType;

            var values = Convert<T>(condition, propertyType);
            var any = values
                .Select(value => Match(value, condition, field))
                .Aggregate(default(Expression), (acc, c) => acc != null ?
                    Expression.OrElse(acc, c) : c);

            return any;
        }

        private List<object> Convert<T>(SearchCondition condition, Type fieldType)
        {
            var convertedType = Activator.CreateInstance(typeof(List<>)
                .MakeGenericType(fieldType)) as Type;
            var converted = new List<object>();
            if (fieldType == typeof(DateTime))
            {
                converted.AddRange((IEnumerable<object>)condition.Values
                    .Select(value => DateTime.Parse(value)));
            }
            else
            {
                var values = condition.Values
                    .Select(value =>
                    {
                        try
                        {
                            return System.Convert
                            .ChangeType(value, fieldType);
                        }
                        catch
                        {
                            return null;
                        }
                    })
                    .Where(value => value != null);
                converted.AddRange(values);
            }

            return converted;

        }

        private Expression Match(object value,
            SearchCondition condition, MemberExpression field)
        {
            switch (condition.Operation)
            {
                // Numeric Operations
                case SearchOperation.LessThan:
                    return Expression.LessThan(field, Expression.Constant(value));
                case SearchOperation.LessThanOrEqual:
                    return Expression.LessThanOrEqual(field, Expression.Constant(value));
                case SearchOperation.GreaterThan:
                    return Expression.GreaterThan(field, Expression.Constant(value));
                case SearchOperation.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(field, Expression.Constant(value));
                case SearchOperation.Between:
                    return Expression.OrElse(Expression.GreaterThanOrEqual(field, Expression.Constant(value)),
                        Expression.LessThanOrEqual(field, Expression.Constant(value)));
                case SearchOperation.NotBetween:
                    return Expression.Not(Expression.OrElse(Expression.GreaterThanOrEqual(field, Expression.Constant(value)),
                        Expression.LessThanOrEqual(field, Expression.Constant(value))));
                case SearchOperation.Null:
                    return Expression.Equal(field, Expression.Constant(null));
                case SearchOperation.NotNull:
                    return Expression.Not(Expression.Equal(field, Expression.Constant(null)));

                // String Operations
                case SearchOperation.Contains:
                    var contains = Expression.Call(field,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(value));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Call(Expression.Call(field,
                            typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                                typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                                    Expression.Constant(value.ToString().ToLower()));
                    }
                    return contains;
                case SearchOperation.NotContains:
                    var notContains = Expression.Not(Expression.Call(field,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(value)));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Not(Expression.Call(Expression.Call(field,
                            typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                                typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                                    Expression.Constant(value.ToString().ToLower())));
                    }
                    return notContains;
                case SearchOperation.StartsWith:
                    var startsWith = Expression.Call(field,
                        typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(value));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Call(Expression.Call(field, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                                typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                                    Expression.Constant(value.ToString().ToLower()));
                    }
                    return startsWith;
                case SearchOperation.NotStartsWith:
                    var notStartsWith = Expression.Not(Expression.Call(field,
                        typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(value)));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Not(Expression.Call(Expression.Call(field, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                                typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                                    Expression.Constant(value.ToString().ToLower())));
                    }
                    return notStartsWith;
                case SearchOperation.EndsWith:
                    var endsWith = Expression.Call(field,
                        typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(value));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Call(Expression.Call(field, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                                typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(value.ToString().ToLower()));
                    }
                    return endsWith;
                case SearchOperation.NotEndsWith:
                    var notEndsWith = Expression.Not(Expression.Call(field,
                        typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(value)));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Not(
                            Expression.Call(Expression.Call(field, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                                typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(value.ToString().ToLower())));
                    }
                    return notEndsWith;
                case SearchOperation.NotEqual:
                    var notEqual = Expression.NotEqual(field, Expression.Constant(value));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.NotEqual(
                            Expression.Call(field, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                            Expression.Constant(value.ToString().ToLower()));
                    }
                    return notEqual;
                default:
                    var equal = Expression.Equal(field, Expression.Constant(value));
                    if (condition.IgnoreCase && value != null && value.GetType() == typeof(string))
                    {
                        return Expression.Equal(
                            Expression.Call(field, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)),
                            Expression.Constant(value.ToString().ToLower()));
                    }
                    return equal;
            }
        }
    }
}
