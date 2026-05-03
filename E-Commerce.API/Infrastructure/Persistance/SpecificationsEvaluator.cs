using Microsoft.EntityFrameworkCore.Query;

namespace Persistance
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuary<TEntity, TKey>(IQueryable<TEntity> inputQuery  // _dbContext.Set<TEntity>()
            , ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _dbContext.Set<TEntity>()
            if (specifications.Criteria is not null) // where(p => p.id == value)
                query = query.Where(specifications.Criteria);

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0) // include
            {
                //foreach (var item in specifications.IncludeExpressions)
                //{
                //    query = query.Include(item);
                //}
                query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));
            }

            if (specifications.IsPageneted)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }

            return query;
        }
    }
}
