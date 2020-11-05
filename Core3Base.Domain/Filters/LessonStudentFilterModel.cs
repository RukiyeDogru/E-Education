using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
   public class LessonStudentFilterModel:FilterModelBase
    {
        public string Term { get; set; }

        public int LessonId { get; set; }

        public int StudentId { get; set; }

        public LessonStudentFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public LessonStudentFilterModel()
        {
        }
    }
}
