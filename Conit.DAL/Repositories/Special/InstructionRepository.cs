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
    public class InstructionRepository : EditRepository<Instruction>, IInstructionRepository
    {
        public InstructionRepository(DbContext context)
            : base(context) { }

        public ApplicationContext ConitContext
        {
            get { return Context as ApplicationContext; }
        }

        public IEnumerable<Instruction> Find(Expression<Func<Instruction, bool>> predicate)
        {
            return ConitContext.Instructions
                    .Where(predicate);
        }

        public Instruction Get(int id)
        {
            return ConitContext.Instructions
                    .SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<Instruction> GetAll()
        {
            return ConitContext.Instructions;
        }

        public Instruction SingleOrDefault(Expression<Func<Instruction, bool>> predicate)
        {
            return ConitContext.Instructions
                    .SingleOrDefault(predicate);
        }
    }
}
