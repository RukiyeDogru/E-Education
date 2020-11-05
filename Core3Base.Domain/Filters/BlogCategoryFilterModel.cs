using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Model.Filters
{
    public class BlogCategoryFilterModel : FilterModelBase
    {

        public string Term { get; set; }

        public BlogCategoryFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }

        }

        public BlogCategoryFilterModel()
        {
        }
    }
}
