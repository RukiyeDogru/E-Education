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
    public class StudentQuestionAnswerService : IStudentQuestionAnswerService
    {
        private readonly IRepository<StudentQuestionAnswer> StudentQuestionAnswerRepository;

        public StudentQuestionAnswerService(IRepository<StudentQuestionAnswer> StudentQuestionAnswerRepository)
        {
            this.StudentQuestionAnswerRepository = StudentQuestionAnswerRepository;

        }
        public ServiceResponse<StudentQuestionAnswer> Add(StudentQuestionAnswer StudentQuestionAnswer)
        {
            var response = new ServiceResponse<StudentQuestionAnswer>();
            if (response.Validation(new StudentQuestionValidation().Validate(StudentQuestionAnswer)))
            {
                response.Result = StudentQuestionAnswerRepository.Insert(StudentQuestionAnswer);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = StudentQuestionAnswerRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                StudentQuestionAnswerRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<StudentQuestionAnswer> GetStudentQuestionAnswerById(int id)
        {
            var response = new ServiceResponse<StudentQuestionAnswer>();

            response.IsSucceeded = true;
            response.Result = StudentQuestionAnswerRepository.GetById(id);

            return response;
        }

        public ServiceResponse<List<StudentQuestionAnswer>> GetStudentQuestionAnswers(StudentQuestionAnswerFilterModel filter)
        {
            var response = new ServiceResponse<List<StudentQuestionAnswer>>();

            response.IsSucceeded = true;

            response.RecordsTotal = StudentQuestionAnswerRepository.ListQueryable.Count();
            response.RecordsFiltered = StudentQuestionAnswerRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = StudentQuestionAnswerRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<StudentQuestionAnswer> Update(StudentQuestionAnswer StudentQuestion)
        {
            var response = new ServiceResponse<StudentQuestionAnswer>();
            if (response.Validation(new StudentQuestionValidation().Validate(StudentQuestion)))
            {
                StudentQuestionAnswerRepository.Detach(StudentQuestion);

                var repositoryResponse = StudentQuestionAnswerRepository.GetById(StudentQuestion.Id);
                if (repositoryResponse != null)
                {

                    response.Result = StudentQuestionAnswerRepository.Update(repositoryResponse);
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
