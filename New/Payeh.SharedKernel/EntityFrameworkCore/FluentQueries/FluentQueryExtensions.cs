using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Payeh.SharedKernel.EntityFrameworkCore.FluentQueries;

/// <summary>
/// Provides advanced filtering, sorting, pagination, and include capabilities for EF Core queries.
/// </summary>
public static class FluentQueryExtensions
{
    /// <summary>
    /// Applies filters to the query.
    /// </summary>
    public static IQueryable<T> ApplyFluentQueryFilters<T>(this IQueryable<T> query, List<FluentQueryFilter> filters)
    {
        if (filters == null || !filters.Any())
            return query;

        foreach (var filter in filters)
        {
            query = ApplyFluentQueryFilter(query, filter);
        }

        return query;
    }

    private static IQueryable<T> ApplyFluentQueryFilter<T>(IQueryable<T> query, FluentQueryFilter filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetNestedPropertyExpression(parameter, filter.Field);
        var propertyType = property.Type;
        var filterValue = Convert.ChangeType(filter.Value, propertyType);
        Expression body;

        switch (filter.Operator.ToLower())
        {
            case "equals":
                body = Expression.Equal(property, Expression.Constant(filterValue));
                break;
            case "contains":
                body = Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(filter.Value));
                break;
            case "startswith":
                body = Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(filter.Value));
                break;
            case "endswith":
                body = Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(filter.Value));
                break;
            case "gt":
                body = Expression.GreaterThan(property, Expression.Constant(filterValue));
                break;
            case "lt":
                body = Expression.LessThan(property, Expression.Constant(filterValue));
                break;
            case "ge":
                body = Expression.GreaterThanOrEqual(property, Expression.Constant(filterValue));
                break;
            case "le":
                body = Expression.LessThanOrEqual(property, Expression.Constant(filterValue));
                break;
            default:
                throw new NotImplementedException($"Operator '{filter.Operator}' is not supported");
        }

        var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
        return query.Where(predicate);
    }

    private static MemberExpression GetNestedPropertyExpression(Expression parameter, string propertyName)
    {
        var parts = propertyName.Split('.');
        Expression current = parameter;

        foreach (var part in parts)
        {
            current = Expression.Property(current, part);
        }

        return (MemberExpression)current;
    }

    public static IQueryable<T> ApplyFluentQuerySorting<T>(this IQueryable<T> query, List<FluentQuerySort> sorts)
    {
        if (sorts == null || !sorts.Any())
            return query;

        var sortExpressions = sorts.Select(sort => $"{sort.Field} {sort.Direction}").ToList();
        string combinedSort = string.Join(", ", sortExpressions);
        return query.OrderBy(combinedSort);
    }

    public static IQueryable<T> ApplyFluentQueryPaging<T>(this IQueryable<T> query, int take, int skip)
    {
        if (skip > 0)
        {
            query = query.Skip(skip);
        }

        if (take > 0)
        {
            query = query.Take(take);
        }

        return query;
    }

    public static IQueryable<T> ApplyFluentQueryIncludes<T>(this IQueryable<T> query, string[] includes) where T : class
    {
        if (includes == null || includes.Length == 0)
            return query;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    public static IQueryable<T> ApplyFluentQuery<T>(this IQueryable<T> query, FluentQuery fluentQuery) where T : class
    {
        query = query.ApplyFluentQueryFilters(fluentQuery.Filters);
        query = query.ApplyFluentQuerySorting(fluentQuery.Sorts);
        query = query.ApplyFluentQueryPaging(fluentQuery.Take, fluentQuery.Skip);
        query = query.ApplyFluentQueryIncludes(fluentQuery.Includes);
        return query;
    }
}
