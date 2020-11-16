using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Domain.Model;
using Core3Base.Domain.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Student.Web.Models;

namespace Student.Web.Controllers
{
    public class TeacherController : Controller
    {
        private ITeacherService _teacherService;
        private ILessonService _lessonService;


        public TeacherController(ITeacherService teacherService, ILessonService lessonService)
        {
            _teacherService = teacherService;
            _lessonService = lessonService;

        }

        [Route("liste")]
        [HttpGet]
        public ActionResult TeacherList()
        {
            var model = new TeacherModel
            {
                Teachers = new List<Core3Base.Infra.Data.Entity.Teacher>()
            };
            return View(model);
        }

        [Route("teacher-query")]
        [HttpPost]
        public JsonResult TeacherListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data = _teacherService.GetAllForDatatables(model);
                if (data.IsSucceeded)
                {
                    return Json(data.Result);
                }
                return Json(data.ErrorMessage);
            }
            catch (Exception e)
            {

                return Json("");
            }
        }

        [Route("ekle")]
        [HttpGet]
        public ActionResult TeacherCreate()
        {
            return RedirectToAction("TeacherEdit", new { TeacherId = 0 });
        }


        [Route("duzenle/{TeacherId}")]
        [HttpGet]
        public ActionResult TeacherEdit(int TeacherId)
        {
            try
            {
                var model = new TeacherModel
                {
                    Teacher = TeacherId == 0 ? new Core3Base.Infra.Data.Entity.Teacher
                    {
                        IsActive = true,
                        Id = 0
                    } : _teacherService.GetTeacherById(TeacherId).Result,
                    LessonGroup = _lessonService.GetAllActiveLesson().Result
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new TeacherModel());
            }
        }

        [Route("duzenle/{TeacherId}")]
        [HttpPost]
        public JsonResult TeacherEdit(int TeacherId, TeacherModel TeacherModel)
        {
            try
            {
                if (TeacherId == 0)
                {
                    Core3Base.Infra.Data.Entity.Teacher Teacher = new Core3Base.Infra.Data.Entity.Teacher
                    {
                        Name = TeacherModel.Teacher.Name,
                        Email = TeacherModel.Teacher.Email,
                        IsActive = TeacherModel.Teacher.IsActive,
                        SurName = TeacherModel.Teacher.SurName,
                        LessonId= TeacherModel.Teacher.LessonId
                    };


                    return Json(_teacherService.Add(Teacher).HttpGetResponse());
                }
                else
                {
                    var Teacher = _teacherService.GetTeacherById(TeacherId).Result;

                    Teacher.Name = TeacherModel.Teacher.Name;
                    Teacher.Email = TeacherModel.Teacher.Email;
                    Teacher.IsActive = TeacherModel.Teacher.IsActive;
                    Teacher.SurName = TeacherModel.Teacher.SurName;
                    Teacher.LessonId = TeacherModel.Teacher.LessonId;
                    Teacher.DateModified = DateTime.Now;

                    return Json(_teacherService.Update(Teacher).HttpGetResponse());


                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public bool TeacherActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var TeacherResponse = _teacherService.GetTeacherById(id);
                if (TeacherResponse.IsSucceeded)
                {
                    var Teacher = TeacherResponse.Result;
                    Teacher.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }
                    _teacherService.Update(Teacher);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        #region Delete

        [Route("secilmislerisil/{ids}")]
        [HttpGet]
        public bool SelectedDelete(string ids)
        {
            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    if (ids.StartsWith(","))
                    {
                        ids = ids.Remove(0, 1);
                    }
                    if (ids.EndsWith(","))
                    {
                        ids = ids.Remove(ids.Length - 1, 1);
                    }
                    var silinecekler = ids.Split(',');
                    for (int i = 0; i < silinecekler.Length; i++)
                    {
                        _teacherService.Delete(Convert.ToInt32(silinecekler[i]));
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [Route("sil/{id}")]
        [HttpGet]
        public bool StudentDelete(int id)
        {
            try
            {
                _teacherService.Delete(id);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
