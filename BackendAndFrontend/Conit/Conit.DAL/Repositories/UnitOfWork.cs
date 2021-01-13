using Conit.DAL.Contexts;
using Conit.DAL.Entities.Identity;
using Conit.DAL.Identity;
using Conit.DAL.Interfaces;
using Conit.DAL.Interfaces.Special;
using Conit.DAL.Repositories.Special;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Conit.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private ClientManager clientManager;

        private CompanyRepository companyRepository;
        private InstructionPageRepository instructionPageRepository;
        private InstructionRepository instructionRepository;
        private PartProductRepository partProductRepository;
        private PartRepository partRepository;
        private ProductRepository productRepository;

        public UnitOfWork(string connectionString)
        {
            context = new ApplicationContext(connectionString);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(
                        new UserStore<ApplicationUser>(context));
                return userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(
                        new RoleStore<ApplicationRole>(context));
                return roleManager;
            }
        }

        public IClientManager ClientManager
        {
            get
            {
                if (clientManager == null)
                    clientManager = new ClientManager(context);
                return clientManager;
            }
        }

        public ICompanyRepository Companies
        {
            get
            {
                if (companyRepository == null)
                    companyRepository = new CompanyRepository(context);
                return companyRepository;
            }
        }

        public IInstructionRepository Instructions
        {
            get
            {
                if (instructionRepository == null)
                    instructionRepository = new InstructionRepository(context);
                return instructionRepository;
            }
        }

        public IInstructionPageRepository InstructionPages
        {
            get
            {
                if (instructionPageRepository == null)
                    instructionPageRepository = new InstructionPageRepository(context);
                return instructionPageRepository;
            }
        }

        public IPartRepository Parts
        {
            get
            {
                if (partRepository == null)
                    partRepository = new PartRepository(context);
                return partRepository;
            }
        }

        public IPartProductRepository PartProducts
        {
            get
            {
                if (partProductRepository == null)
                    partProductRepository = new PartProductRepository(context);
                return partProductRepository;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
