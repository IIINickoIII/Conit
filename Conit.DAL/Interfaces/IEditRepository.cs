using System.Collections.Generic;

namespace Conit.DAL.Interfaces
{
    public interface IEditRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);
    }
}
