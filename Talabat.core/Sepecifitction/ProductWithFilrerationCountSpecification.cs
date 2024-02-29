using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Sepecifitction
{
    public class ProductWithFilrerationCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilrerationCountSpecification(ProductSpecParams productSpec)
         : base(P =>
         (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search)) &&
         (!productSpec.brandId.HasValue || P.ProductBrandId == productSpec.brandId) &&
          (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId)
         ) 
        {


        }
           
    }
}
