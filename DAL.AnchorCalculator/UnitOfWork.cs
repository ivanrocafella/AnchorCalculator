using Core.AnchorCalculator;
using Core.AnchorCalculator.Repositories;
using DAL.AnchorCalculator.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public IAnchorRepository Anchors { get; }
        public IMaterialRepository Materials { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<Type, object>();

            Anchors = new AnchorRepository(context);
            Materials = new MaterialRepository(context);
        }
        public void Complete() => _context.SaveChanges();

        public void BeginTransaction() => _transaction = _context.Database.BeginTransaction();

        public void BeginTransaction(IsolationLevel level) => _transaction = _context.Database.BeginTransaction(level);

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();

            _transaction = null;
        }

        public void CommitTransaction()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            _transaction.Dispose();

            _transaction = null;
        }

#pragma warning disable CS8766 // Допустимость значений NULL для ссылочных типов в типе возвращаемого значения не соответствует неявно реализованному элементу (возможно, из-за атрибутов допустимости значений NULL).
        public IRepository<TEntity>? GetRepository<TEntity>() where TEntity : class
        {
            return _repositories.GetOrAdd(typeof(TEntity), new Repository<TEntity>(_context)) as IRepository<TEntity>;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
