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

    public class SubjectController : Controller
    {
            private ISubjectService _SubjectService;
            private ILessonService _lessonService;


            public SubjectController(ISubjectService SubjectService, ILessonService lessonService)
            {
                _SubjectService = SubjectService;
                _lessonService = lessonService;

            }

            [Route("liste")]
            [HttpGet]
            public ActionResult SubjectList()
            {
                var model = new SubjectModel
                {
                    Subjects = new List<Core3Base.Infra.Data.Entity.Subjects>()
                };
                return View(model);
            }

            [Route("Subject-query")]
            [HttpPost]
            public JsonResult SubjectListQuery(DataTablesModel.DataTableAjaxPostModel model)
            {
                try
                {
                    var data = _SubjectService.GetAllForDatatables(model);
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
            public ActionResult SubjectCreate()
            {
                return RedirectToAction("SubjectEdit", new { SubjectId = 0 });
            }


            [Route("duzenle/{SubjectId}")]
            [HttpGet]
            public ActionResult SubjectEdit(int SubjectId)
            {
                try
                {
                    var model = new SubjectModel
                    {
                        Subject = SubjectId == 0 ? new Core3Base.Infra.Data.Entity.Subjects
                        {
                            IsActive = true,
                            Id = 0
                        } : _SubjectService.GetSubjectById(SubjectId).Result,
                        LessonGroup = _lessonService.GetAllActiveLesson().Result
                    };
                    return View(model);
                }
                catch (Exception e)
                {

                    return View(new SubjectModel());
                }
            }

            [Route("duzenle/{SubjectId}")]
            [HttpPost]
            public JsonResult SubjectEdit(int SubjectId, SubjectModel SubjectModel)
            {
                try
                {
                    if (SubjectId == 0)
                    {
                        Core3Base.Infra.Data.Entity.Subjects Subject = new Core3Base.Infra.Data.Entity.Subjects
                        {
                            
                            SubjectName = SubjectModel.Subject.SubjectName,
                            IsActive = SubjectModel.Subject.IsActive,
                            LessonId = SubjectModel.Subject.LessonId
                        };


                        return Json(_SubjectService.Add(Subject).HttpGetResponse());
                    }
                    else
                    {
                        var Subject = _SubjectService.GetSubjectById(SubjectId).Result;

                        Subject.IsActive = SubjectModel.Subject.IsActive;
                        Subject.SubjectName = SubjectModel.Subject.SubjectName;
                        Subject.LessonId = SubjectModel.Subject.LessonId;
                        Subject.DateModified = DateTime.Now;

                        return Json(_SubjectService.Update(Subject).HttpGetResponse());


                    }
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }

            [HttpGet]
            public bool SubjectActiveChange(int id, bool active)
            {
                try
                {
                    string sonuc = "";
                    var SubjectResponse = _SubjectService.GetSubjectById(id);
                    if (SubjectResponse.IsSucceeded)
                    {
                        var Subject = SubjectResponse.Result;
                        Subject.IsActive = active;
                        if (active)
                        {
                            sonuc = "aktif";
                        }
                        else
                        {
                            sonuc = "pasif";
                        }
                        _SubjectService.Update(Subject);
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
                            _SubjectService.Delete(Convert.ToInt32(silinecekler[i]));
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
                    _SubjectService.Delete(id);
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
 
