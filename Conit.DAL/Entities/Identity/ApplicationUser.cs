using Microsoft.AspNet.Identity.EntityFramework;

namespace Conit.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
