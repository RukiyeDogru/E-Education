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
    public class TeacherController : ControllerBase
    {
        private ITeacherService _TeacherService;

        public TeacherController(ITeacherService TeacherService)
        {
            _TeacherService = TeacherService;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "", typeof(Teacher))]
        public IActionResult Get(int id)
        {
            var TeacherResponse = _TeacherService.GetTeacherById(id);

            if (TeacherResponse.IsSucceeded && TeacherResponse.Result != null)
            {
                return Ok(TeacherResponse.Result);
            }
            return TeacherResponse.HttpGetResponse();

        }

        [HttpPost("add-teacher")]
        [SwaggerResponse(200, "", typeof(Teacher))]
        public IActionResult Add([FromQuery] TeacherViewModel model)
        {
            var TeacherResponse = _TeacherService.Add(new Teacher
            {

                Name = model.Name,
                SurName=model.SurName,
                Email=model.Email,
                LessonId=model.LessonId

            });

            if (TeacherResponse.IsSucceeded && TeacherResponse.Result != null)
            {

                return Ok(TeacherResponse.Result);

            }
            return TeacherResponse.HttpGetResponse();

        }
        [HttpPost("edit-Teacher/{id}")]
        [SwaggerResponse(200, "", typeof(Teacher))]
        public IActionResult Edit(int id, TeacherViewModel model)
        {
            var getTeacher = _TeacherService.GetTeacherById(id);

            if (getTeacher != null && getTeacher.IsSucceeded)

            {
                getTeacher.Result.Name = model.Name;
                getTeacher.Result.SurName = model.SurName;
                getTeacher.Result.Email = model.Email;
                getTeacher.Result.LessonId = model.LessonId;

                var updateResult = _TeacherService.Update(getTeacher.Result);

                if (updateResult.IsSucceeded)

                {
                    return Ok(updateResult.Result);
                }
                return updateResult.HttpGetResponse();

            }
            return getTeacher.HttpGetResponse();
        }


        [HttpPost("delete/{id}")]
        [SwaggerResponse(200, "", typeof(Teacher))]
        public IActionResult DeleteTeacher(int id)
        {
            try

            {
                var result = _TeacherService.Delete(id);

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
        [SwaggerResponse(200, "", typeof(List<Teacher>))]
        public IActionResult GetTeachers(TeacherFilterModel model)
        {
            try

            {
                var result = _TeacherService.GetTeachers(model);
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
