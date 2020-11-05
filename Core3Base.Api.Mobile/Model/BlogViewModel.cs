using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core3Base.Api.Mobile.Model
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string CategoryTitle { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
