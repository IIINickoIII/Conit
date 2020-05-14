using Conit.BLL.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels
{
    public class PartProductViewModel
    {
        public PartProductViewModel()
        {
            Id = 0;
        }

        public int Id { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please, select the Product")]
        public int ProductId { get; set; }

        public ProductDto ProductDto { get; set; }

        [Display(Name = "Part")]
        [Required(ErrorMessage = "Please, select the Part")]
        public int PartId { get; set; }

        public PartDto PartDto { get; set; }

        public IEnumerable<ProductDto> ProductDtos { get; set; }

        public IEnumerable<PartDto> PartDtos { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New" : $"Edit";
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