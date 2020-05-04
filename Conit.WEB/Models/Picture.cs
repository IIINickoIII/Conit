using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Conit.WEB.Models
{
    public class Picture
    {
        public string Name { get; set; }

        [Display(Name = "Picture")]
        public HttpPostedFileBase Upload { get; set; }
    }
}