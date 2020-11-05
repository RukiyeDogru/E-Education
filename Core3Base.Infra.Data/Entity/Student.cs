using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3Base.Infra.Data.Entity
{
    public class Student : BaseEntityWithDate
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public virtual Classs Classs { get; set; }
        public virtual ICollection<LessonStudent> LessonStudents { get; set; }



    }
}
