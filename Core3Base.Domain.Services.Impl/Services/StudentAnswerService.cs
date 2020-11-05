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
    public class StudentAnswerService : IStudentAnswerService
    {
        private readonly IRepository<StudentAnswer> StudentAnswerRepository;

        public StudentAnswerService(IRepository<StudentAnswer> StudentAnswerRepository)
        {
            this.StudentAnswerRepository = StudentAnswerRepository;
        }


        public ServiceResponse<StudentAnswer> Add(StudentAnswer StudentAnswer)
        {
            var response = new ServiceResponse<StudentAnswer>();
            if (response.Validation(new StudentAnswerValidation().Validate(StudentAnswer)))
            {
                response.Result = StudentAnswerRepository.Insert(StudentAnswer);
            }
            return response;

        }
        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = StudentAnswerRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                StudentAnswerRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;

        }

        public ServiceResponse<StudentAnswer> GetStudentAnswerById(int id)
        {

            var response = new ServiceResponse<StudentAnswer>();

            response.IsSucceeded = true;
            response.Result = StudentAnswerRepository.GetById(id);

            return response;
        }

        public ServiceResponse<List<StudentAnswer>> GetStudentAnswers(StudentAnswerFilterModel filter)
        {
            var responce = new ServiceResponse<List<StudentAnswer>>();
            responce.IsSucceeded = true;

            responce.RecordsTotal = StudentAnswerRepository.ListQueryable.Count();
            responce.RecordsFiltered = StudentAnswerRepository.ListQueryable.AddSearchFilters(filter).Count();
            responce.Result = StudentAnswerRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
            return responce;

        }

        public ServiceResponse<StudentAnswer> Update(StudentAnswer StudentAnswer)
        {

            var response = new ServiceResponse<StudentAnswer>();
            if (response.Validation(new StudentAnswerValidation().Validate(StudentAnswer)))
            {
                StudentAnswerRepository.Detach(StudentAnswer);

                var repositoryResponse = StudentAnswerRepository.GetById(StudentAnswer.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.StudentId = StudentAnswer.StudentId;

                    response.Result = StudentAnswerRepository.Update(repositoryResponse);
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

