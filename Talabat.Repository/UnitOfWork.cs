using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core;
using Talabat.core.Entities;
using Talabat.core.Entities.Orders_Aggragtion;
using Talabat.core.Repositiories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;

        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
          _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type= typeof(TEntity).Name; //product ,brand,...

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);

                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;




        }

        public async Task<int> Complete()
        => await context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
          => await context.DisposeAsync();

      
    }
}
