using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class Exam:BaseEntityWithDate
   {
        public string ExamName { get; set; }

        public int? LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lessons { get; set; }

        public int? SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subjects Subject { get; set; }
    }
}
