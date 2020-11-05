using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Core3Base.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core3Base.Infra.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private DbSet<T> _objectSet;
        private bool _disposed;
        protected Core3BaseContext context;

        protected virtual DbSet<T> Entities => _objectSet ?? (_objectSet = context.Set<T>());

        public Repository(Core3BaseContext context)
        {
            this.context = context;
            _objectSet = this.context.Set<T>();
        }

        public IQueryable<T> ListQueryable => Entities.Where(x=>!x.IsDelete);

        public IQueryable<T> ListQueryableNoTracking => Entities.Where(x => !x.IsDelete).AsNoTracking();

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.IsDelete = true;
            Entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                entity.IsDelete = true;
                Entities.Update(entity);
                context.SaveChanges();
            }
        }
        public T GetById(object id)
        {
            return _objectSet.FirstOrDefault(x=>x.Id==(int)id&&!x.IsDelete);
        }

        public IEnumerable<T> GetSql(string sql)
        {
            return Entities.FromSqlInterpolated($"{sql}");
        }

        public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
        {
            return Entities.IncludeMultiple(includes);
        }

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities) // TODO performans?
                Entities.Add(entity);

            context.SaveChanges();
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            //TODO
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                    // Dispose other managed resources.
                }
                //release unmanaged resources.
            }
            _disposed = true;
        }

        public void Detach(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }
    }
}
public static class Extensions
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query;
    }
}