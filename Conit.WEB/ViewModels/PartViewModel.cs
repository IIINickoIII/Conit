using Conit.BLL.Dto;
using Conit.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels
{
    public class PartViewModel
    {
        public PartViewModel()
        {
            Id = 0;
        }

        public Picture Picture { get; set; }

        public string PictureId { get; set; }

        public IEnumerable<ProductDto> ProductDtos { get; set; }

        public int Id { get; set; }

        [Display(Name = "Part Name")]
        [Required(ErrorMessage = "Please, enter the name")]
        [StringLength(60)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please, enter the description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Color")]
        [Required(ErrorMessage = "Please, enter the color")]
        [StringLength(60)]
        public string Color { get; set; }

        [Display(Name = "Material")]
        [Required(ErrorMessage = "Please, enter the material")]
        [StringLength(60)]
        public string Material { get; set; }

        [Display(Name = "Weight")]
        [Required(ErrorMessage = "Please, enter the weight")]
        public int Weight { get; set; }

        [Display(Name = "Height")]
        [Required(ErrorMessage = "Please, enter the height")]
        public int Height { get; set; }

        [Display(Name = "Width")]
        [Required(ErrorMessage = "Please, enter the width")]
        public int Width { get; set; }

        [Display(Name = "Lenght")]
        [Required(ErrorMessage = "Please, enter the Lenght")]
        public int Length { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New part" : "Editing part";
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