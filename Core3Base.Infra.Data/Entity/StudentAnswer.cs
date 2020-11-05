using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class StudentAnswer:BaseEntity
   {
        public int? AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }


        public int? StudentId { get; set; }

        [ForeignKey("StudentId ")]
        public virtual Student Student { get; set; }
        //istrue
        public bool IsTrue { get; set; } = false;

    }
}
