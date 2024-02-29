using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Orders_Aggragtion;
using Talabat.core.Repositiories;

namespace Talabat.core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>()where TEntity:BaseEntity;

        Task<int> Complete();








    }
}
