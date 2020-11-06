using Core3Base.Domain.Model.Filters;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
   public class UserFilterModel:FilterModelBase
    {
        public string Term { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public int UserId { get; set; }
        public UserFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }

        }

        public UserFilterModel()
        {
        }

    }
}
