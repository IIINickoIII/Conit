using Conit.DAL.Entities;

namespace Conit.DAL.Interfaces.Special
{
    public interface IInstructionPageRepository : IReadRepository<InstructionPage>, IEditRepository<InstructionPage>
    {
    }
}
