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
    public class ExamController : ControllerBase
    {
        private IExamService _ExamService;

        public ExamController(IExamService ExamService)
        {
            _ExamService = ExamService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Exam))]
        public IActionResult Get(int id)
        {
            var ExamResponse = _ExamService.GetExamById(id);

            if (ExamResponse.IsSucceeded && ExamResponse.Result != null)
            {
                return Ok(ExamResponse.Result);
            }
            return ExamResponse.HttpGetResponse();

        }

        [HttpPost("add-Exam")]
        [SwaggerResponse(200, "", typeof(Exam))]
        public IActionResult Add([FromQuery] ExamViewModel model)
        {
            var ExamResponse = _ExamService.Add(new Exam
            {
                ExamName = model.ExamName,
                LessonId=model.LessonId

            }) ;

            if (ExamResponse.IsSucceeded && ExamResponse.Result != null)
            {

                return Ok(ExamResponse.Result);

            }
            return ExamResponse.HttpGetResponse();

        }
        [HttpPost("edit-Exam/{id}")]
        [SwaggerResponse(200, "", typeof(Exam))]
        public IActionResult Edit(int id, ExamViewModel model)
        {
            var getExam = _ExamService.GetExamById(id);

            if (getExam != null && getExam.IsSucceeded)
            {
                getExam.Result.ExamName = model.ExamName;
                getExam.Result.LessonId = model.LessonId;

                var updateResult = _ExamService.Update(getExam.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getExam.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Exam))]
        public IActionResult DeleteExam(int id)
        {
            try
            {
                var result = _ExamService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<Exam>))]
        public IActionResult GetExams(ExamFilterModel model)
        {
            try

            {
                var result = _ExamService.GetExams(model);
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
