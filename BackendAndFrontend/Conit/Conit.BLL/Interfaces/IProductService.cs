using Conit.BLL.Dto;
using System.Collections.Generic;

namespace Conit.BLL.Interfaces
{
    public interface IProductService
    {
        ProductDto Get(int productDtoId);

        IEnumerable<ProductDto> GetAll();

        IEnumerable<ProductDto> GetAllByCompanyId(int companyId);

        void Add(ProductDto productDto);

        void Edit(ProductDto productDto);

        void Delete(int productDtoId);
    }
}
