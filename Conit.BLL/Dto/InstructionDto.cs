using System;

namespace Conit.BLL.Dto
{
    public class InstructionDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto ProductDto { get; set; }

        public string Description { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }
    }
}
