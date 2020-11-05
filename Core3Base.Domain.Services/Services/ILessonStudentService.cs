using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface ILessonStudentService
    {
        ServiceResponse<LessonStudent> Add(LessonStudent lessonStudent);
        ServiceResponse<LessonStudent> Update(LessonStudent lessonStudent);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<LessonStudent> GetLessonStudentById(int id);
        ServiceResponse<List<LessonStudent>> GetLessonStudents(LessonStudentFilterModel filter);

   }
}
