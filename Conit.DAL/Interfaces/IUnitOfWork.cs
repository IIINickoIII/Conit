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

        ICompanyRepository Companies { get; }

        IInstructionRepository Instructions { get; }

        IInstructionPageRepository InstructionPages { get; }

        IPartRepository Parts { get; }

        IPartProductRepository PartProducts { get; }

        IProductRepository Products { get; }

        Task SaveAsync();

        void Save();
    }
}
