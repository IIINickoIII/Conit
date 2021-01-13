using Conit.DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace Conit.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        { }
    }
}
