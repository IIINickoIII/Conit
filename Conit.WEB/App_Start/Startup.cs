using Conit.BLL.Infrastructure;
using Conit.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Conit.WEB.App_Start.Startup))]

namespace Conit.WEB.App_Start
{
    public class Startup
    {
        // С помощью фабрики сервисов здесь создается сервис для работы с сервисами
        IUserServiceCreator serviceCreator = new UserServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            // сервис региструется контекстом OWIN
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("ConitConnection");
        }
    }
}