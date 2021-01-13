using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IPartProductService
    {
        PartProductDto Get(int partProductDtoId);

        IEnumerable<PartDto> GetAllPartsOfProduct(int productId);

        IEnumerable<ProductDto> GetAllProductsWithPart(int partId);

        void Add(PartProductDto partProductDto);

        void Edit(PartProductDto partProductDto);

        void Delete(int partId, int productId);
    }
}
