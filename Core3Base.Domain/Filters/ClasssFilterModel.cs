﻿using Core3Base.Domain.Model.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Filters
{
   public class ClasssFilterModel:FilterModelBase
    {
        public string Term { get; set; }
        public string ClasssNameTerm { get; set; }
        public bool? Active { get; set; } 
        public bool? Deleted { get; set; } 
        public ClasssFilterModel(DataTableParameters dataTableParameters)
            : base(dataTableParameters)
        {
            if (dataTableParameters.Search?.Value?.Length > 0)
            {
                Term = dataTableParameters.Search.Value;
            }
        }

        public ClasssFilterModel()
        {
        }

    }
}
