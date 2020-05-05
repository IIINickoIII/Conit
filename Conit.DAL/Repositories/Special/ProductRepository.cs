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
    public class ProductRepository : EditRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context)
            : base(context) { }

        public ApplicationContext ConitContext
        {
            get { return Context as ApplicationContext; }
        }
        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return ConitContext.Products
                    .Where(predicate);
        }

        public Product Get(int id)
        {
            return ConitContext.Products
                    .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return ConitContext.Products.Include(c => c.Company);
        }

        public Product SingleOrDefault(Expression<Func<Product, bool>> predicate)
        {
            return ConitContext.Products
                    .SingleOrDefault(predicate);
        }
    }
}
