using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IInstructionService
    {
        InstructionDto Get(int instructionDtoId);

        IEnumerable<InstructionDto> GetAll();

        IEnumerable<InstructionDto> GetAllByProductId(int productId);

        void Add(InstructionDto instructionDto);

        void Edit(InstructionDto instructionDto);

        void Delete(int instructionDtoId);
    }
}
