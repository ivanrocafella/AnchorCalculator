using Core.AnchorCalculator.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected ApplicationDbContext Context;
        protected DbSet<TEntity> DbSet;
        private bool _disposed;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
        public void Add(TEntity obj) => DbSet.Add(obj);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                Context.Dispose();

            _disposed = true;
        }

        ~Repository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => DbSet.Where(predicate);

        public IQueryable<TEntity> GetAll() => DbSet;

#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        public TEntity GetById(int id) => DbSet.Find(id);

        public void Remove(int id) => DbSet.Remove(DbSet.Find(id));

        public void RemoveByEntity(TEntity entity) => DbSet.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

        public void Update(TEntity obj) => DbSet.Update(obj);
    }
}
