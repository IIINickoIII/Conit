using Conit.DAL.Entities.Identity;
using System;

namespace Conit.DAL.Interfaces.Special
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
