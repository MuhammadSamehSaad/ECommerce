using Talabat.Core.Entites;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithFilterationForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountAsync(ProductSpecParams Parms) : base
            (P =>
                (!Parms.brandId.HasValue || P.BrandId == Parms.brandId) &&
                (!Parms.categoryId.HasValue || P.CategoryId == Parms.categoryId)
            )
        {

        }
    }
}
