using Conit.DAL.Entities;

namespace Conit.DAL.Interfaces.Special
{
    public interface IProductRepository : IReadRepository<Product>, IEditRepository<Product>
    {
    }
}
