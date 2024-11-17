using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        //This Constructor Will Be Used For Createing an Object , That Will Be Used For Get All Products It's Critera = Null
        public ProductWithBrandAndCategorySpecifications(string sort) : base()
        {
            addIncludes();
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAcs":
                        AddOrderBy(P => P.Price); break;
                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price); break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
        {
            addIncludes();
        }

        private void addIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
