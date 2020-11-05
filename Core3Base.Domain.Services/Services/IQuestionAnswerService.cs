using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Base.Domain.Services.Services
{
    public interface IQuestionAnswerService
    {

        ServiceResponse<QuestionsAnswer> Add(QuestionsAnswer QuestionsAnswer);
        ServiceResponse<QuestionsAnswer> Update(QuestionsAnswer QuestionsAnswer);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<QuestionsAnswer> GetQuestionsAnswerById(int id);
        ServiceResponse<List<QuestionsAnswer>> GetQuestionsAnswer(QuestionAnswerFilterModel filter);


    }
}
