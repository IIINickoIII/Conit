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

            Bind<IPartService>().To<PartService>();

            Bind<IProductService>().To<ProductService>();

            Bind<IInstructionService>().To<InstructionService>();

            Bind<IInstructionPageService>().To<InstructionPageService>();

            Bind<IPartProductService>().To<PartProductService>();
        }
    }
}