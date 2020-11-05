using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IStudentQuestionAnswerService
    {
        ServiceResponse<StudentQuestionAnswer> Add(StudentQuestionAnswer StudentQuestionAnswer);
        ServiceResponse<StudentQuestionAnswer> Update(StudentQuestionAnswer StudentQuestionAnswer);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<StudentQuestionAnswer> GetStudentQuestionAnswerById(int id);
        ServiceResponse<List<StudentQuestionAnswer>> GetStudentQuestionAnswers(StudentQuestionAnswerFilterModel filter);

    }
}
