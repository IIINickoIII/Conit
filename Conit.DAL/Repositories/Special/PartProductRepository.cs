using Conit.DAL.Contexts;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces.Special;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Conit.DAL.Repositories.Special
{
    public class PartProductRepository : EditRepository<PartProduct>, IPartProductRepository
    {
        public PartProductRepository(DbContext context)
            : base(context) { }

        public ApplicationContext ConitContext
        {
            get { return Context as ApplicationContext; }
        }

        public IEnumerable<PartProduct> Find(Expression<Func<PartProduct, bool>> predicate)
        {
            return ConitContext.PartProducts
                    .Where(predicate);
        }

        public PartProduct Get(int id)
        {
            return ConitContext.PartProducts
                    .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<PartProduct> GetAll()
        {
            return ConitContext.PartProducts;
        }

        public PartProduct SingleOrDefault(Expression<Func<PartProduct, bool>> predicate)
        {
            return ConitContext.PartProducts
                    .SingleOrDefault(predicate);
        }
    }
}
