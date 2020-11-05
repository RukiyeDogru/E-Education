using Core3Base.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Domain.Model;

namespace Core3Base.Domain.Services.Services
{
    public interface IRoleService
    {
        public ServiceResponse<RoleModel> GetRoleById(int userId, int roleGroupID, Int64 roleID);
        public ServiceResponse<RoleModel> GetRoleListByGroupId(int userId, int roleGroupID);
    }
}
