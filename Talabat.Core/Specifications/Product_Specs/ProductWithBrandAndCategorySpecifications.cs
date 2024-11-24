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
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams Parms) : base
            (P =>
                (!Parms.brandId.HasValue || P.BrandId == Parms.brandId) &&
                (!Parms.categoryId.HasValue || P.CategoryId == Parms.categoryId)
            )
        {
            addIncludes();
            if (!string.IsNullOrEmpty(Parms.sort))
            {
                switch (Parms.sort)
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

            ///Proucts = 100          18
            ///PageSize = 10          10
            ///PageIndex = 4          10
            ///
            ///Skip => 4 * 10 = 40
            ///Take => 10
            
            ApplayPagination(Parms.PageSize * (Parms.PageIndex - 1), Parms.PageSize);
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
