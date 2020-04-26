using AutoMapper;
using Conit.BLL.Dto;
using Conit.BLL.Interfaces;
using Conit.BLL.Models;
using Conit.DAL.Entities.Identity;
using Conit.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Conit.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDto userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                await Database.UserManager.CreateAsync(user, userDto.Password);

                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.RoleId);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration is succesfully completed.", "");
            }
            else
            {
                return new OperationDetails(false, "This login is already taken.", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(IEnumerable<UserDto> userDtos, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }

            foreach (UserDto userDto in userDtos)
            {
                await Create(userDto);
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return Mapper.Map<IQueryable<ApplicationUser>, IEnumerable<UserDto>>(Database.UserManager.Users);
        }

        public UserDto GetUser(string userId)
        {
            var user = Database.UserManager.FindById(userId);
            if (user == null)
                throw new ArgumentException("no user exists with such id");
            return Mapper.Map<ApplicationUser, UserDto>(user);
        }

        public UserDto GetUserByName(string userName)
        {
            var user = Database.UserManager.FindByName(userName);
            if (user == null)
                throw new ArgumentException("no user exists with such id");
            return Mapper.Map<ApplicationUser, UserDto>(user);
        }

        public void ChangeRole(string userId, RoleDto roleDto)
        {
            var user = Database.UserManager.FindById(userId);
            if (user == null)
                throw new ArgumentException("No user exists with such id");
            var role = Database.RoleManager.FindByName(roleDto.Name);
            if (role == null)
                throw new ArgumentException("No role exists with such name");

            Database.UserManager.RemoveFromRole(userId,
                Database.RoleManager.FindById(user.Roles.First()
                        .RoleId)
                    .Name);

            Database.UserManager.AddToRole(userId,
                roleDto.Name);
        }

        public RoleDto GetRole(string roleId)
        {
            var role = Database.RoleManager.FindById(roleId);

            return Mapper.Map<ApplicationRole, RoleDto>(role);
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<RoleDto>>(Database.RoleManager.Roles);
        }

        public void Dispose()
        {
            
        }
    }
}
