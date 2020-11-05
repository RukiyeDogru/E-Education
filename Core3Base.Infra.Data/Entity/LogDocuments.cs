using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class LogDocuments : BaseEntityWithDate
    {
        public string Application { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
        public string UserNo { get; set; }
        public string ActionTaken { get; set; }
        public string UserName { get; set; }
    }
}
