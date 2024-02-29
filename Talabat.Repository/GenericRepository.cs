using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositiories;
using Talabat.core.Sepecifitction;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenericRepository(StoreContext dbContext)
        {
           
                
            this.dbContext = dbContext;
        }
        #region static quires
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
           
            return await dbContext.Set<T>().ToListAsync();

        }
        public async Task<int> GetCountWithSpecAsync(ISpeification<T> Spec)
        {
            return await  ApplySpecification(Spec).CountAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {

            return await dbContext.Set<T>().FindAsync(id);
        }



        #endregion
        public async Task<T> GetByIdwithSpecAsync(ISpeification<T> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllwithSpecAsync(ISpeification<T> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpeification<T> Spec)
        {
            return SpecefictionEvalutor<T>.GetQuery(dbContext.Set<T>(), Spec);
        }

        public async Task Add(T entity)
        => await dbContext.Set<T>().AddAsync(entity);
       

        public void Update(T entity)
        =>dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
        => dbContext.Set<T>().Remove(entity);


    }
}
