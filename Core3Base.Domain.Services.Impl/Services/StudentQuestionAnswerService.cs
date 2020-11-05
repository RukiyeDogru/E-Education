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
        private readonly IRepository<StudentQuestionAnswer> StudentQuestionRepository;

        public StudentQuestionAnswerService(IRepository<StudentQuestionAnswer> StudentQuestionRepository)
        {
            this.StudentQuestionRepository = StudentQuestionRepository;

        }
        public ServiceResponse<StudentQuestionAnswer> Add(StudentQuestionAnswer StudentQuestion)
        {
            var response = new ServiceResponse<StudentQuestionAnswer>();
            if (response.Validation(new StudentQuestionValidation().Validate(StudentQuestion)))
            {
                response.Result = StudentQuestionRepository.Insert(StudentQuestion);
            }
            return response;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = StudentQuestionRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                StudentQuestionRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<StudentQuestionAnswer> GetStudentQuestionById(int id)
        {
            var response = new ServiceResponse<StudentQuestionAnswer>();

            response.IsSucceeded = true;
            response.Result = StudentQuestionRepository.GetById(id);

            return response;
        }

        public ServiceResponse<List<StudentQuestionAnswer>> GetStudentQuestions(StudentQuestionFilterModel filter)
        {
            var response = new ServiceResponse<List<StudentQuestionAnswer>>();

            response.IsSucceeded = true;

            response.RecordsTotal = StudentQuestionRepository.ListQueryable.Count();
            response.RecordsFiltered = StudentQuestionRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = StudentQuestionRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return response;
        }

        public ServiceResponse<StudentQuestionAnswer> Update(StudentQuestionAnswer StudentQuestion)
        {
            var response = new ServiceResponse<StudentQuestionAnswer>();
            if (response.Validation(new StudentQuestionValidation().Validate(StudentQuestion)))
            {
                StudentQuestionRepository.Detach(StudentQuestion);

                var repositoryResponse = StudentQuestionRepository.GetById(StudentQuestion.Id);
                if (repositoryResponse != null)
                {

                    

                    response.Result = StudentQuestionRepository.Update(repositoryResponse);
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
