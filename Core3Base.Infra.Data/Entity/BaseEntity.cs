using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
    public abstract class BaseEntityWithDate : BaseEntity
    {
        public bool IsDeletable { get; set; } = true;
        //[HiddenInput(DisplayValue = false)]
        public DateTime? DateCreated { get; set; }=DateTime.Now;

        //[HiddenInput(DisplayValue = false)]
        public DateTime? DateModified { get; set; }

    }

}
