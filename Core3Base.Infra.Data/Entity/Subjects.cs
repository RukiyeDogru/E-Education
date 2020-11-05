using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class Subjects : BaseEntityWithDate
    {
        public string SubjectName { get; set; }
        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
    }
}
