using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
    public class QuestionExamFilterModel : FilterModelBase
    {
        public string Term { get; set; }
        public int QuestionId { get; set; }
        public int ExamId { get; set; }

        public QuestionExamFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public QuestionExamFilterModel()
        {
        }
    }
}
