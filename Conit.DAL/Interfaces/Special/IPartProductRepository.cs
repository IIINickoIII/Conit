using Conit.DAL.Entities;

namespace Conit.DAL.Interfaces.Special
{
    public interface IPartProductRepository : IReadRepository<PartProduct>, IEditRepository<PartProduct>
    {
    }
}
