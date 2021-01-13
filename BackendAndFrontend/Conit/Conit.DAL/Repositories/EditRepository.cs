using Conit.DAL.Interfaces;
using System.Data.Entity;

namespace Conit.DAL.Repositories
{
    public class EditRepository<TEntity> : IEditRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public EditRepository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
