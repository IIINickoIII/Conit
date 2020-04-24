using Conit.DAL.Entities.Identity;
using System;

namespace Conit.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
