using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Service.Role
{
    public interface IRoleService
    {
        List<ApplicationRole> GetRoles();
        ApplicationRole GetRoleByRoleId(Guid roleId);
        ApplicationRole CreateRole(ApplicationRole role);
        void UpdateRole(ApplicationRole oldRole, ApplicationRole newRole);
        void DeleteRole(ApplicationRole role);
    }
}
