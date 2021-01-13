using Conit.DAL.Entities;

namespace Conit.DAL.Interfaces.Special
{
    public interface IInstructionRepository : IReadRepository<Instruction>, IEditRepository<Instruction>
    {
    }
}
