using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Sepecifitction
{
    public class ProductSpecifications :BaseSpecification<Product>
    {//where(p=>p.productbrandid==brandid && p.producttypeid =typeid)
        //where (p=>true && true)
        //where (p=>p.productbrandid == brandid && true)
        //where(p=> true &&p.producttypeid ==typeid)

        public ProductSpecifications(ProductSpecParams productSpec )
        :base(P=>
        (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search)) &&
        (!productSpec.brandId.HasValue || P.ProductBrandId ==productSpec.brandId)&&
         (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId))
        //get
        {
            includes.Add(p => p.ProductBrand);
            includes.Add(p => p.ProductType);

            if(!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "PriceAsc":
                       AddOrderBy(P => P.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDescanding(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;


                }
            }
            //totalproducts =100;
            //pagesize=10;
            //pageindex=3
            ApplyPagination(productSpec.PageSize*(productSpec.PageIndex-1 ),productSpec.PageSize);
        }
        public ProductSpecifications(int id):base(p=>p.Id==id)
        {
            includes.Add(p => p.ProductBrand);
            includes.Add(p => p.ProductType);
        }
    }
}
