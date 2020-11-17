using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class ExamModel
    {
        public Exam Exam { get; set; }
        public List<Core3Base.Infra.Data.Entity.Exam> Exams { get; set; }
        public IEnumerable<Lesson> LessonGroup { get; set; }

    }
}
