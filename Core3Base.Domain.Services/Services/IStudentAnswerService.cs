using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IStudentAnswerService
    {
        ServiceResponse<StudentAnswer> Add(StudentAnswer StudentAnswer);
        ServiceResponse<StudentAnswer> Update(StudentAnswer StudentAnswer);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<StudentAnswer> GetStudentAnswerById(int id);
        ServiceResponse<List<StudentAnswer>> GetStudentAnswers(StudentAnswerFilterModel filter);
   }
}
