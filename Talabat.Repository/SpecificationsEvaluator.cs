using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;//dbContext.Set<TEntity>()

            if (spec.Criteria is not null)
                query = inputQuery.Where(spec.Criteria);
            //Query = _dbContext.Set<Product>().Where(P => P.Id == 1);

            //Include expresson:
            //1. P => P.Brand
            //2. P => P.Category

            query = spec.Includes.Aggregate(query,(currentQuery , includeExpression) => currentQuery.Include(includeExpression));

            //So The query  will be : 

            //query = _dbContext.Where(spec.Creteria : P => P.Id == 1).Include(spec.Includes: P => P.Brand);
            //query = _dbContext.Where(spec.Creteria : P => P.Id == 1).Include(spec.Includes: P => P.Brand).Include(P => P.Category);



            return query;
        }


    }
}
