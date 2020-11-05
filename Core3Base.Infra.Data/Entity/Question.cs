using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class Question:BaseEntity
   {
        public string Content { get; set; }

        public virtual ICollection<QuestionExam> QuestionExams { get; set; }

        //public int ExamId { get; set; }
        // [ForeignKey("ExamId")]

        // public virtual Exam Exam { get; set; }

        // public int TeacherId { get; set; }
        // [ForeignKey(" TeacherId")]
        // public virtual Teacher Teacher { get; set; }
    }
}
