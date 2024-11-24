using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecParams
    {

        private int pageSize = 5;
        private const int pageMaxSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > pageMaxSize ? pageMaxSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }
    }
}
