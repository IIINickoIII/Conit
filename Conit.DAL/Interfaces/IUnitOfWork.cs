using Conit.DAL.Entities;
using Conit.DAL.Identity;
using Conit.DAL.Interfaces.Special;
using System.Threading.Tasks;

namespace Conit.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IClientManager ClientManager { get; }
        IEditRepository<Company> Companies { get; }
        IEditRepository<Instruction> Instructions { get; }
        IEditRepository<InstructionPage> InstructionPages { get; }
        IEditRepository<Part> Parts { get; }
        IEditRepository<PartProduct> PartProducts { get; }
        IEditRepository<Product> Products { get; }
        Task SaveAsync();
        void Save();
    }
}
