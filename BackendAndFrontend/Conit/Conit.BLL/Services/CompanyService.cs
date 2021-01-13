using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.BLL.Models;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
                throw new ArgumentNullException("companyDto");
            }

            var company = Mapper.Map<Company>(companyDto);

            company.DateOfAdding = DateTime.Now;

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

        public async Task<OperationDetails> AddAsync(CompanyDto companyDto)
        {
            Company companyInDb = await Database.Companies.FindByNameAsync(companyDto.Name);
            if (companyInDb == null)
            {
                var company = Mapper.Map<Company>(companyDto);

                company.DateOfAdding = DateTime.Now;

                Database.Companies.Add(company);
                Database.Save();
                return new OperationDetails(true, "Company was added.", "");
            }
            else
            {
                return new OperationDetails(false, "This name is already taken.", "Name");
            }
        }

        public async Task<OperationDetails> EditAsync(CompanyDto companyDto)
        {
            Company companyInDb = Database.Companies
                .SingleOrDefault(m => m.Name == companyDto.Name);

            if (companyInDb == null ||
                (companyInDb != null &&
                 companyInDb.Id == companyDto.Id))
            {
                companyInDb = Mapper.Map<Company>(companyDto);

                await Database.Companies.UpdateAsync(companyInDb);

                Database.Save();

                return new OperationDetails(true, "Company was updated.", "");
            }
            return new OperationDetails(false, "This name is already taken.", "Name");
        }
    }
}
