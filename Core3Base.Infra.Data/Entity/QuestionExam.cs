using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class QuestionExam:BaseEntityWithDate
    {
        public int? ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }


    }
}
