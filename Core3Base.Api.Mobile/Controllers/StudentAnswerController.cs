using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Api.Mobile.Model;
using Core3Base.Domain.Filters;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Core3Base.Api.Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAnswerController : ControllerBase
    {
        private IStudentAnswerService _StudentAnswerService;

        public StudentAnswerController(IStudentAnswerService StudentAnswerService)
        {
            _StudentAnswerService = StudentAnswerService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(StudentAnswer))]
        public IActionResult Get(int id)
        {
            var StudentAnswerResponse = _StudentAnswerService.GetStudentAnswerById(id);

            if (StudentAnswerResponse.IsSucceeded && StudentAnswerResponse.Result != null)
            {
                return Ok(StudentAnswerResponse.Result);
            }
            return StudentAnswerResponse.HttpGetResponse();

        }

        [HttpPost("add-StudentAnswer")]
        [SwaggerResponse(200, "", typeof(StudentAnswer))]
        public IActionResult Add([FromQuery] StudentAnswerViewModel model)
        {
            var StudentAnswerResponse = _StudentAnswerService.Add(new StudentAnswer
            {
                StudentId=model.StudentId,

            });

            if (StudentAnswerResponse.IsSucceeded && StudentAnswerResponse.Result != null)
            {

                return Ok(StudentAnswerResponse.Result);

            }
            return StudentAnswerResponse.HttpGetResponse();

        }
        [HttpPost("edit-StudentAnswer/{id}")]
        [SwaggerResponse(200, "", typeof(StudentAnswer))]
        public IActionResult Edit(int id, StudentAnswerViewModel model)
        {
            var getStudentAnswer = _StudentAnswerService.GetStudentAnswerById(id);

            if (getStudentAnswer != null && getStudentAnswer.IsSucceeded)

            {
                getStudentAnswer.Result.StudentId = model.StudentId;

                var updateResult = _StudentAnswerService.Update(getStudentAnswer.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getStudentAnswer.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(StudentAnswer))]
        public IActionResult DeleteStudentAnswer(int id)
        {
            try

            {
                var result = _StudentAnswerService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<StudentAnswer>))]
        public IActionResult GetStudentAnswers(StudentAnswerFilterModel model)
        {
            try

            {
                var result = _StudentAnswerService.GetStudentAnswers(model);
                if (result.IsSucceeded)
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
