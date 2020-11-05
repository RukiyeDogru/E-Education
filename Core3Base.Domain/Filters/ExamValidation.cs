using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
    public class ExamFilterModel : FileFilterModel
    {
        public string Term { get; set; }
        public string RepositoryTerm { get; set; }
        public ExamFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }

            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                RepositoryTerm = dataTableParameters.Search.Value;
            }

        }

        public ExamFilterModel()
        {
        }
    }
}
