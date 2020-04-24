using System.Collections.Generic;

namespace Conit.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);
    }
}
