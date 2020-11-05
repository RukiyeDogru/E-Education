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
    public class AnswerService : IAnswerService
    {
        private readonly IRepository<Answer> AnswerRepository;

        public AnswerService(IRepository<Answer> AnswerRepository)
        {
            this.AnswerRepository = AnswerRepository;
        }

        public ServiceResponse<Answer> Add(Answer Answer)
        {
            var response = new ServiceResponse<Answer>();
            if (response.Validation(new AnswerValidation().Validate(Answer)))
            {
                response.Result = AnswerRepository.Insert(Answer);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = AnswerRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                AnswerRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;

        }

        public ServiceResponse<Answer> GetAnswerById(int id)
        {
            var response = new ServiceResponse<Answer>();

            response.IsSucceeded = true;
            response.Result = AnswerRepository.GetById(id);

            return response;

        }

        public ServiceResponse<List<Answer>> GetAnswers(AnswerFilterModel filter)
        {
            var response = new ServiceResponse<List<Answer>>();
            response.IsSucceeded = true;
            response.RecordsTotal = AnswerRepository.ListQueryable.Count();
            response.RecordsFiltered = AnswerRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = AnswerRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<Answer> Update(Answer Answer)
        {

            var response = new ServiceResponse<Answer>();
            if (response.Validation(new AnswerValidation().Validate(Answer)))
            {
                AnswerRepository.Detach(Answer);

                var repositoryResponse = AnswerRepository.GetById(Answer.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Responce =Answer.Responce;

                    response.Result = AnswerRepository.Update(repositoryResponse);
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
