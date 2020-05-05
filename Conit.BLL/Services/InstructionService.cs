using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conit.BLL.Services
{
    public class InstructionService : IInstructionService
    {
        IUnitOfWork Database { get; set; }

        public InstructionService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(InstructionDto instructionDto)
        {
            if (instructionDto == null)
            {
                throw new ArgumentNullException("instructionDto");
            }

            var instruction = Mapper.Map<Instruction>(instructionDto);

            instruction.DateOfAdding = DateTime.Now;

            Database.Instructions.Add(instruction);
            Database.Save();
        }

        public void Delete(int instructionDtoId)
        {
            var instructionInDb = 
                Database.Instructions.Get(instructionDtoId);

            if (instructionInDb == null)
            {
                throw new Exception("No instruction with such Id in the Database.");
            }

            Database.Instructions.Remove(instructionInDb);
            Database.Save();
        }

        public void Edit(InstructionDto instructionDto)
        {
            if (instructionDto == null)
            {
                throw new ArgumentNullException("instructionDto");
            }

            var instruction = Mapper.Map<Instruction>(instructionDto);

            Database.Instructions.Update(instruction);
            Database.Save();
        }

        public InstructionDto Get(int instructionDtoId)
        {
            var instructionInDb = Database.Instructions.Get(instructionDtoId);

            if (instructionInDb == null)
            {
                throw new Exception("No instruction with such Id in the Database.");
            }

            var instructionDto = Mapper.Map<InstructionDto>(instructionInDb);

            return instructionDto;
        }

        public IEnumerable<InstructionDto> GetAll()
        {
            var instructionsInDb = Database.Instructions.GetAll();

            if (instructionsInDb == null)
            {
                throw new Exception("No records in Instructions Table.");
            }

            var instructionDtos =
                Mapper.Map<IEnumerable<InstructionDto>>(instructionsInDb);

            return instructionDtos;
        }
    }
}
