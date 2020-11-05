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
    public class QuestionExamService : IQuestionExamService
    {
        private readonly IRepository<QuestionExam> QuestionExamRepository;

        public QuestionExamService(IRepository<QuestionExam> QuestionExamRepository)
        {
            this.QuestionExamRepository = QuestionExamRepository;
        }

        public ServiceResponse<QuestionExam> Add(QuestionExam QuestionExam)
        {
            var response = new ServiceResponse<QuestionExam>();
            if (response.Validation(new QuestionExamValidation().Validate(QuestionExam)))
            {
                response.Result = QuestionExamRepository.Insert(QuestionExam);
            }
            return response;
        }
        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = QuestionExamRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                QuestionExamRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<List<QuestionExam>> GetQuestionExam(QuestionExamFilterModel filter)
        {
            var response = new ServiceResponse<List<QuestionExam>>();
            response.IsSucceeded = true;

            response.RecordsTotal = QuestionExamRepository.ListQueryable.Count();
            response.RecordsFiltered = QuestionExamRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = QuestionExamRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;

        }

        public ServiceResponse<QuestionExam> GetQuestionExamById(int id)
        {
            var response = new ServiceResponse<QuestionExam>();

            response.IsSucceeded = true;
            response.Result = QuestionExamRepository.GetById(id);

            return response;

        }

        public ServiceResponse<QuestionExam> Update(QuestionExam QuestionsExam)
        {
            var response = new ServiceResponse<QuestionExam>();
            if (response.Validation(new QuestionExamValidation().Validate(QuestionsExam)))
            {
                QuestionExamRepository.Detach(QuestionsExam);

                var repositoryResponse = QuestionExamRepository.GetById(QuestionsExam.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.ExamId = QuestionsExam.ExamId;
                    repositoryResponse.QuestionId = QuestionsExam.QuestionId;
                    response.Result = QuestionExamRepository.Update(repositoryResponse);
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
