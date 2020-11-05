using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class Classs : BaseEntityWithDate
    {
        public string ClassName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
