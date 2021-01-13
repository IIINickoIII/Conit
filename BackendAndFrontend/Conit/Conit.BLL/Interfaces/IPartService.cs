using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IPartService
    {
        PartDto Get(int partDtoId);

        IEnumerable<PartDto> GetAll();

        IEnumerable<PartDto> GetAllPartsByProductId(int productDtoId);

        void Add(PartDto partDto);

        void Edit(PartDto partDto);

        void Delete(int partDtoId);
    }
}
