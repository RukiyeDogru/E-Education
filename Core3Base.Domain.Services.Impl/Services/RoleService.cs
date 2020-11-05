using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core3Base.Domain.Model;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.CrossCutting.AdminIdentity.Entity;
using Core3Base.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core3Base.Domain.Services.Impl.Services
{

    public class RoleService : IRoleService
    {
        private readonly IRepository<UserRole> _userRolesRepository;
        private readonly IRepository<Role> _rolesRepository;
        public RoleService(IRepository<UserRole> userRolesRepository, IRepository<Role> rolesRepository)
        {
            _userRolesRepository = userRolesRepository;
            _rolesRepository = rolesRepository;
        }

        public ServiceResponse<RoleModel> GetRoleById(int userId, int roleGroupID, Int64 roleID)
        {
            var response = new ServiceResponse<RoleModel>();
            RoleModel model = new RoleModel();
            var userRole = _userRolesRepository.ListQueryable
                .Include(r => r.RoleGroup)
                .FirstOrDefault(ur => ur.UserId == userId && ur.RoleGroupId == roleGroupID);
            if (userRole != null)
            {
                if (roleID == (userRole.Roles & roleID))
                {
                    var role = _rolesRepository.ListQueryable.FirstOrDefault(r => r.RoleId == roleID);
                    if (role != null)
                    {
                        model = new RoleModel() { Id = role.Id, RoleName = role.RoleName, RoleGroupID = (int)userRole.RoleGroupId, RoleID = roleID, UserID = userId, GroupName = userRole.RoleGroup.GroupName };
                    }
                }
                response.Result = model;
            }
            return response;
        }

        public ServiceResponse<RoleModel> GetRoleListByGroupId(int userId, int roleGroupID)
        {
            var response = new ServiceResponse<RoleModel>();
            List<RoleModel> model = new List<RoleModel>();
            var userRole = _userRolesRepository.ListQueryable.FirstOrDefault(ur => ur.UserId == userId && ur.RoleGroupId == roleGroupID);
            if (userRole != null)
            {
                var allRoles = _rolesRepository.ListQueryable
                    .Include(r => r.RoleGroup)
                    .Where(r => r.RoleGroupId == roleGroupID);
                foreach (var role in allRoles)
                {
                    if (role.RoleId == (userRole.Roles & role.RoleId))
                    {
                        model.Add(new RoleModel() { Id = role.Id, RoleName = role.RoleName, RoleGroupID = (int)role.RoleGroupId, RoleID = (int)role.RoleId, UserID = userId, GroupName = role.RoleGroup.GroupName });
                    }
                }
                response.List = model;
            }
            return response;
        }
    }
}
