using Conit.DAL.Contexts;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces.Special;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<Company> FindByNameAsync(string companyName)
        {
            var company = await ConitContext.Companies
                .SingleOrDefaultAsync(c => c.Name == companyName);

            return company;
        }

        public async Task<Company> UpdateAsync(Company company)
        {
            using(var context = new ApplicationContext())
            {
                context.Entry(company).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return company;
        }
    }
}
