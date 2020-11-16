using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class AnswerModel
    {
        public Answer Answer { get; set; }
        public List<Answer> Answers { get; set; }
        public IEnumerable<Question> QuestionGroup { get; set; }
    }
}
