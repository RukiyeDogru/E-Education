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
        ServiceResponse<StudentQuestionAnswer> Add(StudentQuestionAnswer StudentQuestion);
        ServiceResponse<StudentQuestionAnswer> Update(StudentQuestionAnswer StudentQuestion);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<StudentQuestionAnswer> GetStudentQuestionById(int id);
        ServiceResponse<List<StudentQuestionAnswer>> GetStudentQuestions(StudentQuestionFilterModel filter);

    }
}
