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
    public class LessonController : ControllerBase
    {
        private ILessonService _LessonService;

        public LessonController(ILessonService LessonService)
        {
            _LessonService = LessonService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Lesson))]
        public IActionResult Get(int id)
        {
            var LessonResponse = _LessonService.GetLessonById(id);

            if (LessonResponse.IsSucceeded && LessonResponse.Result != null)
            {
                return Ok(LessonResponse.Result);
            }
            return LessonResponse.HttpGetResponse();

        }

        [HttpPost("add-Lesson")]
        [SwaggerResponse(200, "", typeof(Lesson))]
        public IActionResult Add([FromQuery] LessonViewModel model)
        {
            var LessonResponse = _LessonService.Add(new Lesson
            {
                LessonName = model.LessonName

            }) ;

            if (LessonResponse.IsSucceeded && LessonResponse.Result != null)
            {

                return Ok(LessonResponse.Result);

            }
            return LessonResponse.HttpGetResponse();

        }
        [HttpPost("edit-lesson/{id}")]
        [SwaggerResponse(200, "", typeof(Lesson))]
        public IActionResult Edit(int id, LessonViewModel model)
        {
            var getLesson = _LessonService.GetLessonById(id);

            if (getLesson != null && getLesson.IsSucceeded)

            {
                getLesson.Result.LessonName = model.LessonName;
               

                var updateResult = _LessonService.Update(getLesson.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getLesson.HttpGetResponse();
        }


        [HttpGet("delete/{Id}")]
        [SwaggerResponse(200, "", typeof(Lesson))]
        public IActionResult DeleteLesson(int Id)
        {
            try

            {
                var result = _LessonService.Delete(Id);

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
        [SwaggerResponse(200, "", typeof(List<Lesson>))]
        public IActionResult GetLessons(LessonFilterModel model)
        {
            try

            {
                var result = _LessonService.GetLessons(model);
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
