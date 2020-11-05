using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;
using Core3Base.Infra.CrossCutting.AdminIdentity.Data;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Infra.CrossCutting.AdminIdentity.Entity
{
   public class Role:BaseEntityWithDate
    {
        public Int64 RoleId { get; set; }
        public string RoleName { get; set; }
        public int RoleGroupId { get; set; }

        [ForeignKey("RoleGroupId")]
        public virtual RoleGroup RoleGroup { get; set; }
    }
}
