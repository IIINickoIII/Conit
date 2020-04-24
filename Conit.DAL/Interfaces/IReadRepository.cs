using System.Collections.Generic;

namespace Conit.DAL.Interfaces
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
    }
}
