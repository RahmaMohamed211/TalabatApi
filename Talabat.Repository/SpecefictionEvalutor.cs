using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Sepecifitction;

namespace Talabat.Repository
{
    public static class SpecefictionEvalutor<TEntity> where TEntity : BaseEntity
    {
        //dbcontext.product or employee 
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpeification<TEntity> spec)
        {
            var query = inputQuery;//context.order

            if(spec.criteria is not null)
            
                query=query.Where(spec.criteria);//p=>p.id ==id 
                                                 //context.products.where(p=>p.productbrandid==brandid)        
                                                 //context.products.where(p=>p.producttypeid ==typeid)
                                                 //context.products.where(p=>p.producttypeid ==typeid)&&(p=>p.productbrandid==brandid)

            if (spec.OrderBy is not null)
                query=query.OrderBy(spec.OrderBy);
            //context.product.orderby(p=>p.name)
            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            //context.product.orderbydescanding(p=>p.price)

            if(spec.IsPaginationEnabled)
                query=query.Skip(spec.Skip).Take(spec.Take);







            query = spec.includes.Aggregate(query,(currentQuery,IncludeExpression)=>currentQuery.Include(IncludeExpression));
            //context.products.where(p=>p.id ==id).include(p=>p.productbrand).include(p=>p.producttype)
            //context.products.orderby(p=>p.name).include(p=>p.productbrand).include(p=>p.producttype)
            //context.product.orderbydescanding(p=>p.price).include(p=>p.productbrand).include(p=>p.producttype)
            //context.products.where(p=>p.id ==id).include(p=>p.productbrand).include(p=>p.producttype)
            // context.products.where(p=>p.id ==id).include(p => p.producttype).include(p => p.producttype)

            // context.products.where(p=>true &&true).skip(0).take(5).include(p => p.producttype).include(p => p.producttype)
            // )

            return query;
        }

    }
}
