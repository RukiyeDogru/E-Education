using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class File : BaseEntityWithDate
    {
        public string Title { get; set; }
        public int FileId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
