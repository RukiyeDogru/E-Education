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
    [Route("sinav")]

    public class ExamController : Controller
    {
        private IExamService _ExamService;
        private ILessonService _lessonService;
        private ISubjectService _subjectService;

        public ExamController(IExamService ExamService, ILessonService lessonService, ISubjectService subjectService)
        {
            _ExamService = ExamService;
            _lessonService = lessonService;
            _subjectService = subjectService;

        }

        [Route("liste")]
        [HttpGet]
        public ActionResult ExamList()
        {
            var model = new ExamModel
            {
                Exams = new List<Core3Base.Infra.Data.Entity.Exam>()
            };
            return View(model);
        }
        [Route("Exam-query")]
        [HttpPost]
        public JsonResult ExamListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data = _ExamService.GetAllForDatatables(model);
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
        public ActionResult ExamCreate()
        {
            return RedirectToAction("ExamEdit", new { ExamId = 0 });
        }


        [Route("duzenle/{ExamId}")]
        [HttpGet]
        public ActionResult ExamEdit(int ExamId)
        {
            try
            {
                var model = new ExamModel
                {
                    Exam = ExamId == 0 ? new Core3Base.Infra.Data.Entity.Exam
                    {
                        IsActive = true,
                        Id = 0
                    } : _ExamService.GetExamById(ExamId).Result,
                    LessonGroup = _lessonService.GetAllActiveLesson().Result
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new ExamModel());
            }
        }


        [Route("duzenle/{ExamId}")]
        [HttpPost]
        public JsonResult ExamEdit(int ExamId, ExamModel ExamModel)
        {
            try
            {
                if (ExamId == 0)
                {
                    Core3Base.Infra.Data.Entity.Exam Exam = new Core3Base.Infra.Data.Entity.Exam
                    {
                        ExamName = ExamModel.Exam.ExamName,
                        IsActive = ExamModel.Exam.IsActive,
                        
                    };

                    return Json(_ExamService.Add(Exam).HttpGetResponse());
                }
                else
                {
                    var Exam = _ExamService.GetExamById(ExamId).Result;
                    Exam.ExamName = ExamModel.Exam.ExamName;
                    Exam.IsActive = ExamModel.Exam.IsActive;
                    Exam.DateModified = DateTime.Now;

                    return Json(_ExamService.Update(Exam).HttpGetResponse());

                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [Route("ekle")]
        [HttpGet]
        public ActionResult ExamAddd(ExamModel examModel)

        {
            return RedirectToAction("ExamEdit", new { ExamId = 0 });

        }


        [Route("yeni-ekle/{ExamId}")]
        [HttpGet]
        public ActionResult ExamAdded(int ExamId)
        {
            try
            {
                var model = new ExamModel
                {
                    Exam = ExamId == 0 ? new Core3Base.Infra.Data.Entity.Exam
                    {
                        IsActive = true,
                        Id = 0
                    } : _ExamService.GetExamById(ExamId).Result,
                    LessonGroup = _lessonService.GetAllActiveLesson().Result,
                    SubjectGroup=_subjectService.GetAllActiveSubject().Result
                };
                return View(model);
            }
            catch (Exception e)
            {
                return View(new ExamModel());
            }
        }


        [Route("duzenle/{ExamId}")]
        [HttpPost]
        public JsonResult ExamAdded(int ExamId, ExamModel ExamModel)
        {
            try
            {
                if (ExamId == 0)
                {
                    Core3Base.Infra.Data.Entity.Exam Exam = new Core3Base.Infra.Data.Entity.Exam
                    {
                        ExamName = ExamModel.Exam.ExamName,
                        IsActive = ExamModel.Exam.IsActive,

                    };

                    return Json(_ExamService.Add(Exam).HttpGetResponse());
                }
                else
                {
                    var Exam = _ExamService.GetExamById(ExamId).Result;
                    Exam.ExamName = ExamModel.Exam.ExamName;
                    Exam.IsActive = ExamModel.Exam.IsActive;
                    Exam.DateModified = DateTime.Now;

                    return Json(_ExamService.Update(Exam).HttpGetResponse());

                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }


        [HttpGet]
        public bool ExamActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var ExamResponse = _ExamService.GetExamById(id);
                if (ExamResponse.IsSucceeded)
                {
                    var Exam = ExamResponse.Result;
                    Exam.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }

                    _ExamService.Update(Exam);
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
                        _ExamService.Delete(Convert.ToInt32(silinecekler[i]));
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
        public bool ExamDelete(int id)
        {
            try
            {
                _ExamService.Delete(id);
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

