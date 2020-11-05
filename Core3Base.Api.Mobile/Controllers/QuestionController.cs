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
    public class QuestionsController : ControllerBase
    {
        private IQuestionService _QuestionsService;

        public QuestionsController(IQuestionService QuestionsService)
        {
            _QuestionsService = QuestionsService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Question))]
        public IActionResult Get(int id)
        {
            var QuestionsResponse = _QuestionsService.GetQuestionById(id);

            if (QuestionsResponse.IsSucceeded && QuestionsResponse.Result != null)
            {
                return Ok(QuestionsResponse.Result);
            }
            return QuestionsResponse.HttpGetResponse();

        }

        [HttpPost("add-Questions")]
        [SwaggerResponse(200, "", typeof(Question))]
        public IActionResult Add([FromQuery] QuestionViewModel model)
        {
            var QuestionsResponse = _QuestionsService.Add(new Question
            {
                Content = model.Content
            });

            if (QuestionsResponse.IsSucceeded && QuestionsResponse.Result != null)
            {
                return Ok(QuestionsResponse.Result);

            }
            return QuestionsResponse.HttpGetResponse();

        }
        [HttpPost("edit-Questions/{id}")]
        [SwaggerResponse(200, "", typeof(Question))]
        public IActionResult Edit(int id, QuestionViewModel model)
        {
            var getQuestions = _QuestionsService.GetQuestionById(id);

            if (getQuestions != null && getQuestions.IsSucceeded)

            {
                getQuestions.Result.Content = model.Content;
                
                var updateResult = _QuestionsService.Update(getQuestions.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getQuestions.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Question))]
        public IActionResult DeleteQuestions(int id)
        {
            try
            {
                var result = _QuestionsService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<Question>))]
        public IActionResult GetQuestionss(QuestionFilterModel model)
        {
            try

            {
                var result = _QuestionsService.GetQuestions(model);
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
