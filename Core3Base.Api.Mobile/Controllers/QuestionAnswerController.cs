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
    public class QuestionAnswerController : ControllerBase
    {
        private IQuestionAnswerService _questionAnswerService;

        public QuestionAnswerController(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult Get(int id)
        {
            var QuestionAnswerResponse = _questionAnswerService.GetQuestionsAnswerById(id);

            if (QuestionAnswerResponse.IsSucceeded && QuestionAnswerResponse.Result != null)
            {
                return Ok(QuestionAnswerResponse.Result);
            }
            return QuestionAnswerResponse.HttpGetResponse();

        }

        [HttpPost("add-questionsOption")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult Add([FromQuery] QuestionAnswerViewModel model)
        {
            var QuestionsOptionResponse = _questionAnswerService.Add(new QuestionsAnswer
            {
                Content=model.Content,
                QuestionsId=model.QuestionsId
                
            });

            if (QuestionsOptionResponse.IsSucceeded && QuestionsOptionResponse.Result != null)
            {

                return Ok(QuestionsOptionResponse.Result);

            }
            return QuestionsOptionResponse.HttpGetResponse();

        }
        [HttpPost("edit-questionsOption/{id}")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult Edit(int id, QuestionAnswerViewModel model)
        {
            var getQuestionsOption = _questionAnswerService.GetQuestionsAnswerById(id);

            if (getQuestionsOption != null && getQuestionsOption.IsSucceeded)

            {
                getQuestionsOption.Result.Content = model.Content;
                getQuestionsOption.Result.QuestionsId = model.QuestionsId;

                var updateResult = _questionAnswerService.Update(getQuestionsOption.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getQuestionsOption.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(QuestionsAnswer))]
        public IActionResult DeleteQuestionsOption(int id)
        {
            try
            {
                var result = _questionAnswerService.Delete(id);

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
        public IActionResult GetQuestionOption(QuestionAnswerFilterModel model)
        {
            try

           {  
                var result = _questionAnswerService.GetQuestionsAnswer(model);
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
