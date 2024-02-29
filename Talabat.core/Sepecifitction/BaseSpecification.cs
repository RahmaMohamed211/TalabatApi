using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Sepecifitction
{
    public class BaseSpecification<T> : ISpeification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> criteria { get ; set; }//where
        public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>(); //include
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public int Skip { get; set; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get ; set; }

        public BaseSpecification()
        {
           
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria) //p =>p.id ==id
        {
            this.criteria = criteria;
            


        }
        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }
        public void AddOrderByDescanding(Expression<Func<T, object>> OrderByDescanding)
        {
            this.OrderByDesc = OrderByDescanding;
        }

        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }



    }


}
