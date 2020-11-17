using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Web.Models
{
    public class QuestionModel
    {
        public Question Question { get; set; }
        public List<Question> Questions { get; set; }


    }
}
