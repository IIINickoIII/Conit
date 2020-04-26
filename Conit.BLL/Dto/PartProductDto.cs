using System;

namespace Conit.BLL.Dto
{
    public class PartProductDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public int PartId { get; set; }

        public PartDto Part { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }
    }
}
