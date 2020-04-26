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
    public class InstructionPageRepository : EditRepository<InstructionPage>, IInstructionPageRepository
    {
        public InstructionPageRepository(DbContext context)
            : base(context) { }

        public ApplicationContext ConitContext
        {
            get { return Context as ApplicationContext; }
        }

        public IEnumerable<InstructionPage> Find(Expression<Func<InstructionPage, bool>> predicate)
        {
            return ConitContext.InstructionPages
                    .Where(predicate)
                    .Include(i => i.Part);
        }

        public InstructionPage Get(int id)
        {
            return ConitContext.InstructionPages
                    .Include(i => i.Part)
                    .SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<InstructionPage> GetAll()
        {
            return ConitContext.InstructionPages
                    .Include(i => i.Part);
        }

        public InstructionPage SingleOrDefault(Expression<Func<InstructionPage, bool>> predicate)
        {
            return ConitContext.InstructionPages
                    .Include(i => i.Part)
                    .SingleOrDefault(predicate);
        }
    }
}
