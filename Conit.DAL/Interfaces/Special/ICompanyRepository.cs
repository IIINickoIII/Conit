using Conit.DAL.Entities;

namespace Conit.DAL.Interfaces.Special
{
    public interface ICompanyRepository : IReadRepository<Company>, IEditRepository<Company>
    {
    }
}
