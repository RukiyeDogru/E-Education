using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class StudentModel
    {
        public Core3Base.Infra.Data.Entity.Student Student { get; set; }
        public List<Core3Base.Infra.Data.Entity.Student> Students { get; set; }
    }
}
