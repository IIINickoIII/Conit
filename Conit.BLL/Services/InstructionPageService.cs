using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Conit.BLL.Services
{
    public class InstructionPageService : IInstructionPageService
    {
        IUnitOfWork Database { get; set; }

        public InstructionPageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(InstructionPageDto instructionPageDto)
        {
            if (instructionPageDto == null)
            {
                throw new ArgumentNullException("instructionPageDto");
            }

            var instructionPage = Mapper.Map<InstructionPage>(instructionPageDto);

            Database.InstructionPages.Add(instructionPage);
            Database.Save();
        }

        public void Delete(int instructionPageDtoId)
        {
            var instructionPageInDb =
                Database.InstructionPages.Get(instructionPageDtoId);

            if (instructionPageInDb == null)
            {
                throw new Exception("No InstructionPage with such id in Database");
            }

            Database.InstructionPages.Remove(instructionPageInDb);
            Database.Save();
        }

        public void Edit(InstructionPageDto instructionPageDto)
        {
            if (instructionPageDto == null)
            {
                throw new ArgumentNullException("instructionPageDto");
            }

            var instructionPage =
                Mapper.Map<InstructionPage>(instructionPageDto);

            Database.InstructionPages.Update(instructionPage);
            Database.Save();
        }

        public InstructionPageDto Get(int instructionPageDtoId)
        {
            var instructionPageInDb =
                Database.InstructionPages.Get(instructionPageDtoId);

            if (instructionPageInDb == null)
            {
                throw new Exception("No InstructionPage with such Id in Database.");
            }

            var instructionPageDto =
                Mapper.Map<InstructionPageDto>(instructionPageInDb);

            return instructionPageDto;
        }

        public IEnumerable<InstructionPageDto> GetAll()
        {
            var instructionPagesInDb = Database.InstructionPages.GetAll();

            if (instructionPagesInDb == null)
            {
                throw new Exception("No records in InstructionPages Table.");
            }

            var instructionPageDtos =
                Mapper.Map<IEnumerable<InstructionPageDto>>(instructionPagesInDb);

            return instructionPageDtos;
        }

        public IEnumerable<InstructionPageDto> GetAll(int instructionId)
        {
            var instructionPagesInDb = Database.InstructionPages
                .GetAll()
                .Where(p => p.InstructionId == instructionId)
                .OrderBy(p => p.PageNumber);

            if (instructionPagesInDb == null)
            {
                throw new Exception("No records in InstructionPages Table.");
            }

            var instructionPageDtos =
                Mapper.Map<IEnumerable<InstructionPageDto>>(instructionPagesInDb);

            return instructionPageDtos;
        }
    }
}
