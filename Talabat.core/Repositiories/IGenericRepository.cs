using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Sepecifitction;

namespace Talabat.core.Repositiories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task <IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id); 

        Task<IReadOnlyList<T>> GetAllwithSpecAsync(ISpeification<T> Spec);

        Task<T> GetByIdwithSpecAsync(ISpeification<T> Spec);

        Task<int> GetCountWithSpecAsync(ISpeification<T> Spec);

        Task Add(T entity);

        void Update(T entity);

        void Delete(T entity);




    }
}
