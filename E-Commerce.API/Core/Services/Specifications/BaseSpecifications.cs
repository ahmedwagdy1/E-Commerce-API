using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> 
        : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region criteria [Where]
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion

        #region include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion

        #region Sorting (OrderBy - OrderByDescending)

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        protected Expression<Func<TEntity, object>>? AddOrderBy(Expression<Func<TEntity, object>>? orderBy) => OrderBy = orderBy;
        protected Expression<Func<TEntity, object>>? AddOrderByDescending(Expression<Func<TEntity, object>>? orderByDescending) => OrderByDescending = orderByDescending;
        #endregion

        #region Pagenations
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPageneted { get; private set; }

        protected void ApplyPagenations(int pageIndex, int pageSize)
        {
            // 1 , 8
            IsPageneted = true;
            Take = pageSize; // 8
            Skip = (pageIndex - 1) * pageSize; // (1 - 1) * 8 = 0
        }
        #endregion
    }
}
