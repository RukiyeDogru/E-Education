using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class LessonStudent:BaseEntityWithDate
    {

        public int LessonId { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        public int StudentId { get; set; }

        [ForeignKey("StudentId ")]
        public virtual Student Student { get; set; }



    }
}
