using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class Answer:BaseEntityWithDate
    {
        public string Responce { get; set; }
        public int? QuestionsId { get; set; }
        [ForeignKey("QuestionsId")]
        public virtual Question Question { get; set; }

    }
}
