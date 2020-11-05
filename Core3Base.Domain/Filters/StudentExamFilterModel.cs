using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
   public class StudentExamFilterModel : FilterModelBase
    {
        public string Term { get; set; }

        public int ExamId { get; set; }

        public int StudentId { get; set; }

        public StudentExamFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public StudentExamFilterModel()
        {
        }














    }
}
