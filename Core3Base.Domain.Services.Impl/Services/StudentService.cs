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
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> studentRepository;
        
        public StudentService(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
          
        }
        public ServiceResponse<Student> Add(Student student)
        {
            var response = new ServiceResponse<Student>();
            if (response.Validation(new StudentValidation().Validate(student)))
            {
                response.Result = studentRepository.Insert(student);
            }

            return response;

        }

        public ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>();
            var repoResponse = studentRepository.GetById(id);

            response.Result = false;
            if (repoResponse != null)
            {
                studentRepository.Delete(repoResponse);
                response.Result = true;
            }
            else
            {
                response.SetError("Kayıt bulunamadı");
            }
            return response;
        }

        public ServiceResponse<Student> GetStudentById(int id)
        {
            var response = new ServiceResponse<Student>();

            response.IsSucceeded = true;
            response.Result = studentRepository.GetById(id);
             
            return response;
        }

        public ServiceResponse<List<Student>> GetStudents(StudentFilterModel filter)
        {
            var response = new ServiceResponse<List<Student>>();

            response.IsSucceeded = true;

            response.RecordsTotal = studentRepository.ListQueryable.Count();
            response.RecordsFiltered = studentRepository.ListQueryable.AddSearchFilters(filter).Count();
            response.Result = studentRepository.ListQueryable.AddSearchFilters(filter).AddOrderAndPageFilters(filter).ToList();
             return response;
        }

        public ServiceResponse<Student> Update(Student student)
        {
            var response = new ServiceResponse<Student>();
            if (response.Validation(new StudentValidation().Validate(student)))
            {
                studentRepository.Detach(student);

                var repositoryResponse = studentRepository.GetById(student.Id);
                if (repositoryResponse != null)
                {
                    repositoryResponse.Name = student.Name;
                    repositoryResponse.Surname = student.Surname;
                    repositoryResponse.Email = student.Email;
                    repositoryResponse.ClassId = student.ClassId;
                
                    response.Result = studentRepository.Update(repositoryResponse);
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
