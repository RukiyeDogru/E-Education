using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
   public class Blog: BaseEntityWithDate
    {
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = true;
        public int ImageId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual BlogCategory Category { get; set; }

    }
}
