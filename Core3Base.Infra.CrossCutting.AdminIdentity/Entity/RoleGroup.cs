using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Infra.CrossCutting.AdminIdentity.Entity;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Infra.CrossCutting.AdminIdentity.Entity
{
   public class RoleGroup:BaseEntityWithDate
    {
        public string GroupName { get; set; }
    }
}
