using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IQuestionService
    {
        ServiceResponse<Question> Add(Question question);
        ServiceResponse<Question> Update(Question Question);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<Question> GetQuestionById(int id);
        ServiceResponse<List<Question>> GetQuestions(QuestionFilterModel filter);

    }
}
