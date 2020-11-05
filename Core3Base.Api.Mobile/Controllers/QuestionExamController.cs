
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
    public class QuestionExamController : ControllerBase
    {
        private IQuestionExamService _QuestionExamService;

        public QuestionExamController(IQuestionExamService QuestionExamService)
        {
            _QuestionExamService = QuestionExamService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult Get(int id)
        {
            var QuestionExamResponse = _QuestionExamService.GetQuestionExamById(id);

            if (QuestionExamResponse.IsSucceeded && QuestionExamResponse.Result != null)
            {
                return Ok(QuestionExamResponse.Result);
            }
            return QuestionExamResponse.HttpGetResponse();

        }

        [HttpPost("add-questionsExam")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult Add([FromQuery] QuestionExamViewModel model)
        {
            var QuestionsExamResponse = _QuestionExamService.Add(new QuestionExam
            {
                QuestionId=model.QuestionId,
                ExamId=model.ExamId
            });

            if (QuestionsExamResponse.IsSucceeded && QuestionsExamResponse.Result != null)
            {

                return Ok(QuestionsExamResponse.Result);

            }
            return QuestionsExamResponse.HttpGetResponse();

        }
        [HttpPost("edit-questionsExam/{id}")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult Edit(int id, QuestionExamViewModel model)
        {
            var getQuestionsExam = _QuestionExamService.GetQuestionExamById(id);

            if (getQuestionsExam != null && getQuestionsExam.IsSucceeded)
            {
                getQuestionsExam.Result.ExamId = model.ExamId;
                getQuestionsExam.Result.QuestionId = model.QuestionId;

                var updateResult = _QuestionExamService.Update(getQuestionsExam.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getQuestionsExam.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult DeleteQuestionsOption(int id)
        {
            try
            {
                var result = _QuestionExamService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<QuestionsAnswer>))]
        public IActionResult GetQuestionOption(QuestionExamFilterModel model)
        {
            try

            {
                var result = _QuestionExamService.GetQuestionExam(model);
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
