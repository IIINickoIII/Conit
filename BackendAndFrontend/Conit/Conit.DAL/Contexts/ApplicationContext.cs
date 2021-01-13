using Conit.DAL.Entities;
using Conit.DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Conit.DAL.Contexts
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("ConitConnection") { }

        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public DbSet<InstructionPage> InstructionPages { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<PartProduct> PartProducts { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
