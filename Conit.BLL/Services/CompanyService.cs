using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Conit.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        IUnitOfWork Database { get; set; }

        public CompanyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                throw new ArgumentNullException("compantDto");
            }

            var company = Mapper.Map<Company>(companyDto);

            Database.Companies.Add(company);
            Database.Save();
        }

        public void Delete(int companyDtoId)
        {
            var companyInDb = Database.Companies.Get(companyDtoId);

            if (companyInDb == null)
            {
                throw new Exception("No company with such Id in the Database.");
            }

            Database.Companies.Remove(companyInDb);
            Database.Save();
        }

        public void Edit(CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                throw new ArgumentNullException("companyDto");
            }

            var company = Mapper.Map<Company>(companyDto);

            Database.Companies.Update(company);
            Database.Save();
        }

        public CompanyDto Get(int companyDtoid)
        {
            var companyInDb = Database.Companies.Get(companyDtoid);

            if (companyInDb == null)
            {
                throw new Exception("No company with such Id in the Database.");
            }

            var companyDto = Mapper.Map<CompanyDto>(companyInDb);

            return companyDto;
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            var companiesInDb = Database.Companies.GetAll();

            if (companiesInDb == null)
            {
                throw new Exception("No records in Companies Table.");
            }

            var companyDtos =
                Mapper.Map<IEnumerable<CompanyDto>>(companiesInDb);

            return companyDtos;
        }
    }
}
