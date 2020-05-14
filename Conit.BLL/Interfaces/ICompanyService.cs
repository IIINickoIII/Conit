using Conit.BLL.Dto;
using Conit.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Conit.BLL.Interfaces
{
    public interface ICompanyService
    {
        CompanyDto Get(int companyDtoId);

        IEnumerable<CompanyDto> GetAll();

        void Add(CompanyDto companyDto);

        void Edit(CompanyDto companyDto);

        void Delete(int companyDtoId);

        Task<OperationDetails> AddAsync(CompanyDto companyDto);

        Task<OperationDetails> EditAsync(CompanyDto companyDto);
    }
}
