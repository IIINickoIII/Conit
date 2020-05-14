using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.DAL.Entities;
using Conit.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Conit.BLL.Services
{
    public class PartService : IPartService
    {
        IUnitOfWork Database { get; set; }

        public PartService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Add(PartDto partDto)
        {
            if (partDto == null)
            {
                throw new ArgumentNullException("partDto");
            }

            var part = Mapper.Map<Part>(partDto);

            part.DateOfAdding = DateTime.Now;

            Database.Parts.Add(part);
            Database.Save();
        }

        public void Delete(int partDtoId)
        {
            var partInDb = Database.Parts.Get(partDtoId);

            if (partInDb == null)
            {
                throw new Exception("No part with such Id in the Database.");
            }

            Database.Parts.Remove(partInDb);
            Database.Save();
        }

        public void Edit(PartDto partDto)
        {
            if (partDto == null)
            {
                throw new ArgumentNullException("partDto");
            }

            var part = Mapper.Map<Part>(partDto);

            Database.Parts.Update(part);
            Database.Save();
        }

        public PartDto Get(int partDtoId)
        {
            var partInDb = Database.Parts.Get(partDtoId);

            if (partInDb == null)
            {
                throw new Exception("No part with such Id in the Database.");
            }

            var partDto = Mapper.Map<PartDto>(partInDb);

            return partDto;
        }

        public IEnumerable<PartDto> GetAll()
        {
            var partsInDb = Database.Parts.GetAll();

            if (partsInDb == null)
            {
                throw new Exception("No records in Parts Table.");
            }

            var partDtos =
                Mapper.Map<IEnumerable<PartDto>>(partsInDb);

            return partDtos;
        }

        public IEnumerable<PartDto> GetAllPartsByProductId(int productDtoId)
        {
            throw new NotImplementedException();
        }
    }
}
