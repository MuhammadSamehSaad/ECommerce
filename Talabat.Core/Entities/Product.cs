using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Entites
{
    public class Product : BaseEntity
    {
        public string  Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        //[ForeignKey(nameof(Product.Brand)] //Will Be Handeled By FluentAPI
        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; } //Navigational Property[One]

        //[ForeignKey(nameof(Product.Category))]
        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; }



    }
}
