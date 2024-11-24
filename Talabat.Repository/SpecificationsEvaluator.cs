using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;//dbContext.Set<TEntity>()

            //Query = _dbContext.Set<Product>().Where(P => P.Id == 1);
            if (spec.Criteria is not null)
                query = inputQuery.Where(spec.Criteria);

            //Her We But The Sorting Like(OrderBy() | OrderByDesc) 
            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }


            //Include expresson:
            //1. P => P.Brand
            //2. P => P.Category

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            //So The query  will be : 

            //query = _dbContext.Where(spec.Creteria : P => P.Id == 1).Include(spec.Includes: P => P.Brand);
            //query = _dbContext.Where(spec.Creteria : P => P.Id == 1).Include(spec.Includes: P => P.Brand).Include(P => P.Category);



            return query;
        }


    }
}
