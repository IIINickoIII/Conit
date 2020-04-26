using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IPartProductService
    {
        PartProductDto Get(int partProductDtoId);

        IEnumerable<PartProductDto> GetAll();

        void Add(PartProductDto partProductDto);

        void Edit(PartProductDto partProductDto);

        void Delete(int partProductDtoId);
    }
}
