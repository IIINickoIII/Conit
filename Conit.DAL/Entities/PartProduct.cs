using System;

namespace Conit.DAL.Entities
{
    public class PartProduct
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        public int? PartId { get; set; }

        public Part Part { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }
    }
}
