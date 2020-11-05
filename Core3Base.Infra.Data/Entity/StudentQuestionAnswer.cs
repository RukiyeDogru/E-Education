using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class StudentQuestionAnswer:BaseEntityWithDate
   {
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId ")]
        public virtual Student Student { get; set; }


        public int AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

   }
}
