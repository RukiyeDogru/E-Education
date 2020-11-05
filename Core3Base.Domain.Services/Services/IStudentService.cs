using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IStudentService
   {
        ServiceResponse<Student> Add(Student student);
        ServiceResponse<Student> Update(Student student);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Student> GetStudentById(int id);
        ServiceResponse<List<Student>> GetStudents(StudentFilterModel filter);

   }
}
