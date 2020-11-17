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
    [Route("dersler")]
    public class LessonController : Controller
    {
        private ILessonService _LessonService;


        public LessonController(ILessonService LessonService)
        {
            _LessonService = LessonService;

        }

        [Route("liste")]
        [HttpGet]
        public ActionResult LessonList()
        {
            var model = new LessonModel
            {
                Lessons = new List<Core3Base.Infra.Data.Entity.Lesson>()
            };
            return View(model);
        }

        [Route("Lesson-query")]
        [HttpPost]
        public JsonResult LessonListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data = _LessonService.GetAllForDatatables(model);
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
        public ActionResult LessonCreate()
        {
            return RedirectToAction("LessonEdit", new { LessonId = 0 });
        }


        [Route("duzenle/{LessonId}")]
        [HttpGet]
        public ActionResult LessonEdit(int LessonId)
        {
            try
            {
                var model = new LessonModel
                {
                    Lesson = LessonId == 0 ? new Core3Base.Infra.Data.Entity.Lesson
                    {
                        IsActive = true,
                        Id = 0
                    } : _LessonService.GetLessonById(LessonId).Result,
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new LessonModel());
            }
        }

        [Route("duzenle/{LessonId}")]
        [HttpPost]
        public JsonResult LessonEdit(int LessonId, LessonModel LessonModel)
        {
            try
            {
                if (LessonId == 0)
                {
                    Core3Base.Infra.Data.Entity.Lesson Lesson = new Core3Base.Infra.Data.Entity.Lesson
                    {
                        LessonName = LessonModel.Lesson.LessonName,
                        IsActive = LessonModel.Lesson.IsActive,
                    };


                    return Json(_LessonService.Add(Lesson).HttpGetResponse());
                }
                else
                {
                    var Lesson = _LessonService.GetLessonById(LessonId).Result;

                    Lesson.LessonName = LessonModel.Lesson.LessonName;
                    Lesson.IsActive = LessonModel.Lesson.IsActive;
                    Lesson.DateModified = DateTime.Now;

                    return Json(_LessonService.Update(Lesson).HttpGetResponse());


                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public bool LessonActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var LessonResponse = _LessonService.GetLessonById(id);
                if (LessonResponse.IsSucceeded)
                {
                    var Lesson = LessonResponse.Result;
                    Lesson.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }
                    _LessonService.Update(Lesson);
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
                        _LessonService.Delete(Convert.ToInt32(silinecekler[i]));
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
                _LessonService.Delete(id);
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
