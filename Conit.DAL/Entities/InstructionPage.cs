using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conit.DAL.Entities
{
    public class InstructionPage
    {
        public int Id { get; set; }

        public int InstructionId { get; set; }

        public Instruction Instruction { get; set; }

        public int PartId { get; set; }

        public Part Part { get; set; }

        public int PageNumber { get; set; }

        public string PictureId { get; set; }
    }
}
