using Conit.BLL.Interfaces;
using Conit.BLL.Services;
using Conit.DAL.Repositories;

namespace Conit.BLL.Infrastructure
{
    public class UserServiceCreator : IUserServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}
