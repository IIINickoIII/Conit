using Conit.BLL.Interfaces;
using Conit.BLL.Services;
using Ninject.Modules;

namespace Conit.WEB.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ICompanyService>().To<CompanyService>();
        }
    }
}