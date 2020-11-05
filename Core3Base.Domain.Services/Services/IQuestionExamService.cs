using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
   public interface IQuestionExamService
    {
        ServiceResponse<QuestionExam> Add(QuestionExam QuestionExam);
        ServiceResponse<QuestionExam> Update(QuestionExam QuestionExam);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<QuestionExam> GetQuestionExamById(int id);
        ServiceResponse<List<QuestionExam>> GetQuestionExam(QuestionExamFilterModel filter);

    }
}
