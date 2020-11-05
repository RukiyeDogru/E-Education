using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Domain.Validations;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<Question> questionRepository;

        public QuestionService(IRepository<Question> questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public ServiceResponse<Question> Add(Question question)
        {

            var response = new ServiceResponse<Question>();
            if (response.Validation(new QuestionValidation().Validate(question)))
            {
                response.Result = questionRepository.Insert(question);
            }

            return response;

        }

        public ServiceResponse<bool> Delete(int id)
        {
            var responce = new ServiceResponse<bool>();

            var repoResponse = questionRepository.GetById(id);

            responce.Result = false;
            if (repoResponse != null)
            {
                questionRepository.Delete(repoResponse);
                responce.Result = true;
            }
            else
            {
                responce.SetError("Kayıt bulunamadı");
            }
            return responce;

        }

        public ServiceResponse<Question> GetQuestionById(int id)
        {
                var response = new ServiceResponse<Question>();

                response.IsSucceeded = true;
                response.Result = questionRepository.GetById(id);

                return response;

            
        }

        public ServiceResponse<List<Question>> GetQuestions(QuestionFilterModel filter)
        {

            var response = new ServiceResponse<List<Question>>();
            response.IsSucceeded = true;

            response.RecordsTotal = questionRepository.ListQueryable.Count();
            response.RecordsFiltered = questionRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = questionRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;


        }

        public ServiceResponse<Question> Update(Question Question)
        {
            var response = new ServiceResponse<Question>();
            if (response.Validation(new QuestionValidation().Validate(Question)))
            {
                questionRepository.Detach(Question);

                var repositoryResponse = questionRepository.GetById(Question.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Content =Question.Content;

                    response.Result = questionRepository.Update(repositoryResponse);
                }
                else
                {
                    response.SetError("Veri Bulunamadı");
                }
            }
            return response;


        }
    }
}
