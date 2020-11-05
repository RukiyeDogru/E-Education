using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class QuestionsAnswer:BaseEntity
   { 
        public string Option { get; set; }
        public string Content { get; set; }
        public int? QuestionsId { get; set; }
        [ForeignKey("QuestionsId")]
        public virtual Question Question {get; set;}
        public int? AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public virtual Answer Answer {get; set;}
   }
}
