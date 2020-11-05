using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Model.Filters
{
    public class FilterModelBase
    {
        public int Page { get; set; }
        public int PageLimit { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        //Dynamic Linq
        public string OrderBy { get; set; }
        public string OrderByDescending { get; set; } = "";

        public FilterModelBase(DataTableParameters dataTableParameters)
        {
            if (dataTableParameters.Order.Count > 0)
            {
                OrderBy = dataTableParameters.Columns[dataTableParameters.Order[0].Column].Name + " " +
                          dataTableParameters.Order[0].Dir;
            }
            else
            {
                OrderBy = "Id DESC ";
            }

            if (dataTableParameters.Start > 1)
                Page = dataTableParameters.Start / dataTableParameters.Length;
            if (dataTableParameters.Length > 1)
                PageLimit = dataTableParameters.Length;

        }

        public FilterModelBase()
        {
            Page = 0;
            PageLimit = 0;
            OrderBy = "";
        }
    }
}
