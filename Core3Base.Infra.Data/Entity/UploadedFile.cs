using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class UploadedFile : BaseEntityWithDate
    {
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public bool IsNeedAuth { get; set; }
        public string ReferanceId { get; set; }
        public string ReferanceFunction { get; set; }
        public string ContentType { get; set; }
    }
}
