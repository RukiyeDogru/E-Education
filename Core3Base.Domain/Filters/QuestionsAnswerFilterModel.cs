using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
    public class QuestionAnswerFilterModel : FilterModelBase
    {
        public string Term { get; set; }
        public int QuestionOptionId { get; set; }
        public QuestionAnswerFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public QuestionAnswerFilterModel()
        {
        }

    }
}
