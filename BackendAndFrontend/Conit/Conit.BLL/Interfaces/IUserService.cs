using Conit.BLL.Dto;
using Conit.BLL.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Conit.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(IEnumerable<UserDto> userDtos, List<string> roles);
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUser(string userId);
        UserDto GetUserByName(string userName);
        void ChangeRole(string userId, RoleDto roleDto);
        RoleDto GetRole(string roleId);
        IEnumerable<RoleDto> GetRoles();
    }
}
