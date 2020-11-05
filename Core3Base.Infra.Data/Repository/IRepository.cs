using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Infra.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity

    {
        IQueryable<T> ListQueryable { get; }
        IQueryable<T> ListQueryableNoTracking { get; }  
        T GetById(object id);
        T Insert(T entity);
        void Insert(IEnumerable<T> entities);
        T Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Detach(T entity);
        IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetSql(string sql);
    }
}
