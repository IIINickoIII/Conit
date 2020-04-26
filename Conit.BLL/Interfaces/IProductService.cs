using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IProductService
    {
        ProductDto Get(int productDtoId);

        IEnumerable<ProductDto> GetAll();

        void Add(ProductDto productDto);

        void Edit(ProductDto productDto);

        void Delete(int productDtoId);
    }
}
