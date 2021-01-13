namespace Conit.DAL.Entities
{
    public class InstructionPage
    {
        public int Id { get; set; }

        public int? InstructionId { get; set; }

        public Instruction Instruction { get; set; }

        public int? PartId { get; set; }

        public Part Part { get; set; }

        public int PageNumber { get; set; }

        public string PictureId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
