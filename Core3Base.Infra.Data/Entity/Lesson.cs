using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class Lesson:BaseEntityWithDate
   {
        public string LessonName { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<LessonStudent> LessonStudents { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

   }
}
