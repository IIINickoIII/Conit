using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface ICompanyService
    {
        CompanyDto Get(int companyDtoId);

        IEnumerable<CompanyDto> GetAll();

        void Add(CompanyDto companyDto);

        void Edit(CompanyDto companyDto);

        void Delete(int companyDtoId);
    }
}
