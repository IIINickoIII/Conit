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
    public class PartRepository : EditRepository<Part>, IPartRepository
    {
        public PartRepository(DbContext context)
            : base(context) { }

        public ApplicationContext ConitContext
        {
            get { return ConitContext; }
        }

        public IEnumerable<Part> Find(Expression<Func<Part, bool>> predicate)
        {
            return ConitContext.Parts
                    .Where(predicate);
        }

        public Part Get(int id)
        {
            return ConitContext.Parts
                    .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Part> GetAll()
        {
            return ConitContext.Parts;
        }

        public Part SingleOrDefault(Expression<Func<Part, bool>> predicate)
        {
            return ConitContext.Parts
                    .SingleOrDefault(predicate);
        }
    }
}
