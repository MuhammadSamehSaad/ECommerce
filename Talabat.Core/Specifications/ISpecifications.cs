using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }//Used For Where(); That Will Return Bool : P => P.Id == id

        public List<Expression<Func<T, object>>> Includes { get; set; }//Used For Include();

        //Her Two Props For OrderBy(P => P.##) And OrderByDesc(P => P.##)

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }

        public bool IsPaginationEnabled { get; set; }

    }
}
