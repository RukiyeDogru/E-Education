using Core3Base.Domain.Filters;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Domain.Validations;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class QuestionsAnswerService : IQuestionAnswerService
    {
        private readonly IRepository<QuestionsAnswer> QuestionsAnswerRepository;

        public QuestionsAnswerService(IRepository<QuestionsAnswer> QuestionsAnswerRepository)
        {
            this.QuestionsAnswerRepository = QuestionsAnswerRepository;
        }

        public ServiceResponse<QuestionsAnswer> Add(QuestionsAnswer QuestionsAnswer)
        {
            var response = new ServiceResponse<QuestionsAnswer>();
            if (response.Validation(new QuestionAnswerValidation().Validate(QuestionsAnswer)))
            {
                response.Result = QuestionsAnswerRepository.Insert(QuestionsAnswer);
            }
            return response;
        }
        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = QuestionsAnswerRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                QuestionsAnswerRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<List<QuestionsAnswer>> GetQuestionsAnswer(QuestionAnswerFilterModel filter)
        {
            var response = new ServiceResponse<List<QuestionsAnswer>>();
            response.IsSucceeded = true;

            response.RecordsTotal = QuestionsAnswerRepository.ListQueryable.Count();
            response.RecordsFiltered = QuestionsAnswerRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = QuestionsAnswerRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;

        } 
        public ServiceResponse<QuestionsAnswer> GetQuestionsAnswerById(int id)
        {
            var response = new ServiceResponse<QuestionsAnswer>();

            response.IsSucceeded = true;
            response.Result = QuestionsAnswerRepository.GetById(id);

            return response;

        }

        public ServiceResponse<QuestionsAnswer> Update(QuestionsAnswer QuestionsOption)
        {
            var response = new ServiceResponse<QuestionsAnswer>();
            if (response.Validation(new QuestionAnswerValidation().Validate(QuestionsOption)))
            {
                QuestionsAnswerRepository.Detach(QuestionsOption);

                var repositoryResponse = QuestionsAnswerRepository.GetById(QuestionsOption.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.QuestionsId = QuestionsOption.QuestionsId;

                    response.Result = QuestionsAnswerRepository.Update(repositoryResponse);
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
