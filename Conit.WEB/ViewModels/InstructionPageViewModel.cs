using Conit.BLL.Dto;
using Conit.WEB.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels
{
    public class InstructionPageViewModel
    {
        public InstructionPageViewModel()
        {
            Id = 0;
        }

        public int Id { get; set; }

        public string PictureId { get; set; }

        public Picture Picture { get; set; }

        [Display(Name = "Instruction")]
        [Required(ErrorMessage = "Please, select the Instruction")]
        public int InstructionId { get; set; }

        public InstructionDto InstructionDto { get; set; }

        public IEnumerable<InstructionDto> InstructionDtos { get; set; }

        [Display(Name = "Part")]
        [Required(ErrorMessage = "Please, select the Part")]
        public int PartId { get; set; }

        public PartDto PartDto { get; set; }

        public IEnumerable<PartDto> PartDtos { get; set; }

        [Display(Name = "Page number")]
        [Required(ErrorMessage = "Please, select the Page number")]
        public int PageNumber { get; set; }

        public bool IsDeleted { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New page" : $"Editing page {PageNumber}";
            }
        }

        public bool IsNewCreated
        {
            get
            {
                return (Id == 0);
            }
        }
    }
}