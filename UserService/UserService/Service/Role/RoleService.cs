using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Service.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public ApplicationRole CreateRole(ApplicationRole role)
        {
            if (!_roleManager.RoleExistsAsync(role.Name).Result)
            {
                _roleManager.CreateAsync(role).Wait();
                return role;

            }
            throw new Exception("Role must be unique");
        }

        public void DeleteRole(ApplicationRole role)
        {
            role.Deleted = true;
            var result = _roleManager.UpdateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new Exception("Server level error");
            }
        }

        public ApplicationRole GetRoleByRoleId(Guid roleId)
        {
            return _roleManager.FindByIdAsync(roleId.ToString()).Result;
        }

        public List<ApplicationRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public void UpdateRole(ApplicationRole oldRole, ApplicationRole newRole)
        {
            if (_roleManager.RoleExistsAsync(newRole.Name).Result)
            {
                throw new Exception("Role must be unique");

            }
            oldRole.Description = newRole.Description;
            oldRole.Name = newRole.Name;
            var result = _roleManager.UpdateAsync(oldRole).Result;
        }
    }
}
