using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
    public class TeacherFilterModel : FilterModelBase
    {
        public string Term { get; set; }
        public string NameTerm { get; set; }
        public string SurNameTerm { get; set; }
        public string Email { get; set; }

        public int LessonId { get; set; }
        public TeacherFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }

        }

        public TeacherFilterModel()
        {
        }

    }
}
