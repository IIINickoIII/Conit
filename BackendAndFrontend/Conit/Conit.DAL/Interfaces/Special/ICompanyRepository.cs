using Conit.DAL.Entities;
using System.Threading.Tasks;

namespace Conit.DAL.Interfaces.Special
{
    public interface ICompanyRepository : IReadRepository<Company>, IEditRepository<Company>
    {
        Task<Company> FindByNameAsync(string companyName);
        Task<Company> UpdateAsync(Company company);
    }
}
