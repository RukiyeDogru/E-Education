using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class LessonModel
    {
        public Lesson Lesson { get; set; }
        public List<Lesson> Lessons { get; set; }

    }
}
