using Conit.BLL.Dto;
using Conit.WEB.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Id = 0;
        }

        public Picture Picture { get; set; }

        public string PictureId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, enter the product name")]
        [StringLength(80)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please, enter the product description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please, select the company")]
        public int CompanyId { get; set; }

        public CompanyDto CompanyDto { get; set; }

        public IEnumerable<CompanyDto> CompanyDtos { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please, enter the product gategory")]
        [StringLength(80)]
        public string Category { get; set; }

        public bool IsDeleted { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New product" : "Editing product";
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