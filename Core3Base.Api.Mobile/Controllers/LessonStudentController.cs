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
    public class LessonStudentController : ControllerBase
    {
        private ILessonStudentService _LessonStudentService;

        public LessonStudentController(ILessonStudentService LessonStudentService)
        {
            _LessonStudentService = LessonStudentService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(LessonStudent))]
        public IActionResult Get(int id)
        {
            var LessonStudentResponse = _LessonStudentService.GetLessonStudentById(id);

            if (LessonStudentResponse.IsSucceeded && LessonStudentResponse.Result != null)
            {
                return Ok(LessonStudentResponse.Result);
            }
            return LessonStudentResponse.HttpGetResponse();

        }


        [HttpPost("add-lessonStudent")]
        [SwaggerResponse(200, "", typeof(LessonStudent))]
        public IActionResult Add([FromQuery] LessonStudentViewModel model)
        {
            var LessonStudentResponse = _LessonStudentService.Add(new LessonStudent
            { 
                LessonId=model.LessonId,
                StudentId=model.StudentId
                
            });

            if (LessonStudentResponse.IsSucceeded && LessonStudentResponse.Result != null)
            {

                return Ok(LessonStudentResponse.Result);

            }
            return LessonStudentResponse.HttpGetResponse();

        }
        [HttpPost("edit-LessonStudent/{id}")]
        [SwaggerResponse(200, "", typeof(LessonStudent))]
        public IActionResult Edit(int id, LessonStudentViewModel model)
        {
            var getLessonStudent = _LessonStudentService.GetLessonStudentById(id);

            if (getLessonStudent != null && getLessonStudent.IsSucceeded)

            {
                getLessonStudent.Result.StudentId = model.StudentId;
                getLessonStudent.Result.LessonId = model.LessonId;
                var updateResult = _LessonStudentService.Update(getLessonStudent.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getLessonStudent.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(LessonStudent))]
        public IActionResult DeleteLessonStudent(int id)
        {
            try
            {
                var result = _LessonStudentService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<LessonStudent>))]
        public IActionResult GetLessonStudents(LessonStudentFilterModel model)
        {
            try

            {
                var result = _LessonStudentService.GetLessonStudents(model);
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
