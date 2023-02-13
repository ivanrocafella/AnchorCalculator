using Core.AnchorCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator
{
    public interface IUnitOfWork : IDisposable
    {
        IAnchorRepository Anchors { get; }
        IMaterialRepository Materials { get; }

        void Complete();
        void BeginTransaction();
        void BeginTransaction(IsolationLevel level);
        void RollbackTransaction();
        void CommitTransaction();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
