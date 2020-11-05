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
    public class AnswerController : ControllerBase
    {
        private IAnswerService _AnswerService;

        public AnswerController(IAnswerService AnswerService)
        {
            _AnswerService = AnswerService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Answer))]
        public IActionResult Get(int id)
        {
            var AnswerResponse = _AnswerService.GetAnswerById(id);

            if (AnswerResponse.IsSucceeded && AnswerResponse.Result != null)
            {
                return Ok(AnswerResponse.Result);
            }
            return AnswerResponse.HttpGetResponse();

        }

        [HttpPost("add-Answer")]
        [SwaggerResponse(200, "", typeof(Answer))]
        public IActionResult Add([FromQuery] AnswerViewModel model)
        {
            var AnswerResponse = _AnswerService.Add(new Answer
            {
                QuestionsId =model.QuestionsId,
                Responce=model.Responce,

            });

            if (AnswerResponse.IsSucceeded && AnswerResponse.Result != null)
            {

                return Ok(AnswerResponse.Result);

            }
            return AnswerResponse.HttpGetResponse();

        }
        [HttpPost("edit-Answer/{id}")]
        [SwaggerResponse(200, "", typeof(Answer))]
        public IActionResult Edit(int id, AnswerViewModel model)
        {
            var getAnswer = _AnswerService.GetAnswerById(id);

            if (getAnswer != null && getAnswer.IsSucceeded)

            {
                getAnswer.Result.Responce = model.Responce;
                getAnswer.Result.QuestionsId = model.QuestionsId;


                var updateResult = _AnswerService.Update(getAnswer.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getAnswer.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Answer))]
        public IActionResult DeleteAnswer(int id)
        {
            try
            {
                var result = _AnswerService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<Answer>))]
        public IActionResult GetAnswers(AnswerFilterModel model)
        {
            try

            {
                var result = _AnswerService.GetAnswers(model);
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
