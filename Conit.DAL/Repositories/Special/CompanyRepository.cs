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
    public class CompanyRepository : EditRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context)
            : base(context) { }

        public ApplicationContext ConitContext
        {
            get { return Context as ApplicationContext; }
        }

        public IEnumerable<Company> Find(Expression<Func<Company, bool>> predicate)
        {
            return ConitContext.Companies
                    .Where(predicate);
        }

        public Company Get(int id)
        {
            return ConitContext.Companies
                    .SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Company> GetAll()
        {
            return ConitContext.Companies;
        }

        public Company SingleOrDefault(Expression<Func<Company, bool>> predicate)
        {
            return ConitContext.Companies
                    .SingleOrDefault(predicate);
        }
    }
}
