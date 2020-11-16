using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class TeacherModel
    {

        public Teacher Teacher { get; set; }
        public List<Teacher> Teachers { get; set; }
        public IEnumerable<Lesson> LessonGroup { get; set; }



    }
}
