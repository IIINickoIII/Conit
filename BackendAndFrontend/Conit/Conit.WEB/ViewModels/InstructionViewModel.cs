using Conit.BLL.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels
{
    public class InstructionViewModel
    {
        public InstructionViewModel()
        {
            Id = 0;
        }

        public int Id { get; set; }

        public IEnumerable<InstructionPageDto> Pages { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please, select the product")]
        public int ProductId { get; set; }

        public ProductDto ProductDto { get; set; }

        public IEnumerable<ProductDto> ProductDtos { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please, enter the description")]
        [StringLength(255)]
        public string Description { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New instruction" : "Editing instruction";
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