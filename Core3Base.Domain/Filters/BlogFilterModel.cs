using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Model.Filters
{
    public class BlogFilterModel : FilterModelBase
    {
        public string Term { get; set; }
        public string CategoryTerm { get; set; }
        public string CategoryNotTerm { get; set; }
        public int CategoryId { get; set; }
        public BlogFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }

        }

        public BlogFilterModel()
        {
        }
    }
}
