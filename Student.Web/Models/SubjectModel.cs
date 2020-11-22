using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class SubjectModel
    {

        public Subjects Subject { get; set; }
        public List<Core3Base.Infra.Data.Entity.Subjects> Subjects { get; set; }
        public IEnumerable<Lesson> LessonGroup { get; set; }
       

    }
}
