using Conit.WEB.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Conit.WEB.ViewModels
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            Id = 0;
        }

        public int Id { get; set; }

        public Picture Picture { get; set; }

        public string PictureId { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please, enter the name")]
        [StringLength(60)]
        public string Name { get; set; }

        [Display(Name = "About company")]
        [Required(ErrorMessage = "Please, enter the description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Please, enter the phone number")]
        [StringLength(12)]
        [RegularExpression("[0-9]{12}")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Contact Person")]
        [Required(ErrorMessage = "Please, enter the contact person's name")]
        [StringLength(60)]
        public string ContactName { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New company" : "Editing company";
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