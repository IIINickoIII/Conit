﻿using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IInstructionPageService
    {
        InstructionPageDto Get(int instructionPageDtoId);

        IEnumerable<InstructionPageDto> GetAll();

        void Add(InstructionPageDto instructionPageDto);

        void Edit(InstructionPageDto instructionPageDto);

        void Delete(int instructionPageDtoId);
    }
}
