using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class BlogCategory : BaseEntityWithDate
    {
        public string Title { get; set; }
        public int ImageId { get; set; }
    }
}
