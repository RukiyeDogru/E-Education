using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
  public class StudentAnswerFilterModel: FilterModelBase
    {
        public string Term { get; set; }
        public string StudentAnswerId { get; set; }
        public StudentAnswerFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public StudentAnswerFilterModel()
        {
        }
    }
}
