using Conit.DAL.Entities;

namespace Conit.DAL.Interfaces.Special
{
    public interface IPartRepository : IReadRepository<Part>, IEditRepository<Part>
    {
    }
}
