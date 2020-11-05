using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class Teacher:BaseEntityWithDate
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
    }
}
