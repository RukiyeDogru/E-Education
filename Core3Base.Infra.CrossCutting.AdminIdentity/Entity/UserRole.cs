using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core3Base.Infra.CrossCutting.AdminIdentity.Data;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Infra.CrossCutting.AdminIdentity.Entity
{
   public class UserRole:BaseEntityWithDate
    {
        public int UserId { get; set; }
        public int RoleGroupId { get; set; }
        public Int64 Roles { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("RoleGroupId")]
        public virtual RoleGroup RoleGroup { get; set; }

    }
}
