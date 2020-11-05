﻿using System;
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
    public class StudentQuestionController : ControllerBase
    {
        private IStudentQuestionAnswerService _StudentQuestionService;

        public StudentQuestionController(IStudentQuestionAnswerService StudentQuestionService)
        {
            _StudentQuestionService = StudentQuestionService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(StudentQuestionAnswer))]
        public IActionResult Get(int id)
        {
            var StudentQuestionResponse = _StudentQuestionService.GetStudentQuestionById(id);

            if (StudentQuestionResponse.IsSucceeded && StudentQuestionResponse.Result != null)
            {
                return Ok(StudentQuestionResponse.Result);
            }
            return StudentQuestionResponse.HttpGetResponse();

        }

        [HttpPost("add-StudentQuestion")]
        [SwaggerResponse(200, "", typeof(StudentQuestionAnswer))]
        public IActionResult Add([FromQuery] StudentQuestionViewModel model)
        {
            var StudentQuestionResponse = _StudentQuestionService.Add(new StudentQuestionAnswer
            {
                StudentId = model.StudentId,
                QuestionId=model.QuestionId

            });

            if (StudentQuestionResponse.IsSucceeded && StudentQuestionResponse.Result != null)
            {

                return Ok(StudentQuestionResponse.Result);

            }
            return StudentQuestionResponse.HttpGetResponse();

        }
        [HttpPost("edit-StudentQuestion/{id}")]
        [SwaggerResponse(200, "", typeof(StudentQuestionAnswer))]
        public IActionResult Edit(int id, StudentQuestionViewModel model)
        {
            var getStudentQuestion = _StudentQuestionService.GetStudentQuestionById(id);

            if (getStudentQuestion != null && getStudentQuestion.IsSucceeded)

            {
                getStudentQuestion.Result.StudentId = model.StudentId;

                var updateResult = _StudentQuestionService.Update(getStudentQuestion.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getStudentQuestion.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(StudentQuestionAnswer))]
        public IActionResult DeleteStudentQuestion(int id)
        {
            try

            {
                var result = _StudentQuestionService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<StudentQuestionAnswer>))]
        public IActionResult GetStudentQuestions(StudentQuestionFilterModel model)
        {
            try

            {
                var result = _StudentQuestionService.GetStudentQuestions(model);
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
