using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Conit.DAL.Interfaces
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
