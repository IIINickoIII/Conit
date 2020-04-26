namespace Conit.BLL.Dto
{
    public class InstructionPageDto
    {
        public int Id { get; set; }

        public int InstructionId { get; set; }

        public InstructionDto InstructionDto { get; set; }

        public int PartId { get; set; }

        public PartDto PartDto { get; set; }

        public int PageNumber { get; set; }

        public string PictureId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
