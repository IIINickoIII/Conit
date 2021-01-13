namespace Conit.BLL.Interfaces
{
    public interface IUserServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}
