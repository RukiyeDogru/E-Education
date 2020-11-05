using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
    public class StudentFilterModel : FilterModelBase
    {
        public string Term { get; set; }
        public string NameTerm { get; set; }
        public string SurNameTerm { get; set; }
        public int? ClassId { get; set; }
        public StudentFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public StudentFilterModel()
        {
        }

    }
}