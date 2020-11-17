using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
  public class QuestionOption
    {
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
        public int? OptionId { get; set; }
        [ForeignKey("OptionId")]
        public virtual Option Option { get; set; }


    }
}
