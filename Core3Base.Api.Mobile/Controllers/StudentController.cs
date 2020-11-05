using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core3Base.Api.Mobile.Model;
using Core3Base.Domain.Filters;
using Core3Base.Domain.Services.Impl.Helper;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Core3Base.Api.Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Student))]
        public IActionResult Get(int id)
        {
            var studentResponse = _studentService.GetStudentById(id);

            if (studentResponse.IsSucceeded && studentResponse.Result != null)
            {
                return Ok(studentResponse.Result);
            }
            return studentResponse.HttpGetResponse();

        }

        [HttpPost("add-student")]
        [SwaggerResponse(200, "", typeof(Student))]
        public IActionResult Add([FromQuery] StudentViewModel model)
        {
            var studentResponse = _studentService.Add(new Student { 
            
              Name=model.Name,
              Surname=model.Surname,
              Email=model.Email,
              ClassId=model.ClassId
            
            });

            if (studentResponse.IsSucceeded && studentResponse.Result != null)
            {
               
                return Ok( studentResponse.Result);

            }
            return studentResponse.HttpGetResponse();

        }
        [HttpPost("edit-student/{id}")]
        [SwaggerResponse(200, "", typeof(Student))]
        public IActionResult Edit(int id, StudentViewModel model)
        {
            var getStudent = _studentService.GetStudentById(id);

            if(getStudent!=null&&getStudent.IsSucceeded)

            {
                getStudent.Result.ClassId = model.ClassId;
                getStudent.Result.Name = model.Name;
                getStudent.Result.Surname = model.Surname;
                getStudent.Result.Email = model.Email;

                var updateResult = _studentService.Update(getStudent.Result);

                if(updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                    return updateResult.HttpGetResponse();

            }
            return getStudent.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Student))]
        public IActionResult DeleteStudent(int id)
        {
            try

            {
                var result = _studentService.Delete(id);

                if (result.IsSucceeded)
                {
                    return Ok(result.Result);
                }
                return BadRequest(result.HttpGetResponse());
                //return result.HttpGetResponse();
            }
            catch (Exception e)

            {
                
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        [SwaggerResponse(200, "", typeof(List<Student>))]
        public IActionResult GetStudents(StudentFilterModel model)
        {
            try 
            
            {
                var result = _studentService.GetStudents(model);
                if(result.IsSucceeded)
                {
                    return Ok(result.Result);
                    
                }
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
     
}

