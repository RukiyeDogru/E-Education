using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3Base.Domain.Model;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Student.Web.Models;

namespace Student.Web.Controllers
{
    public class AnswerController : Controller
    {
        private IAnswerService _answerService;
        private IQuestionService QuestionService;

        public AnswerController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            QuestionService = questionService;
        }
        [Route("liste")]
        [HttpGet]
        public ActionResult AnswerList()
        {
            var model = new AnswerModel
            {
                Answers = new List<Core3Base.Infra.Data.Entity.Answer>()
            };
            return View(model);
        }


        [Route("answer-query")]
        [HttpPost]
        public JsonResult AnswerListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data = _answerService.GetAllForDatatables(model);
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
        public ActionResult AnswerCreate()
        {
            return RedirectToAction("AnswerEdit", new { StudentId = 0 });
        }


        [Route("duzenle/{StudentId}")]
        [HttpGet]
        public ActionResult AnswerEdit(int AnswerId)
        {
            try
            {

                var model = new AnswerModel
                {
                    Answer = AnswerId == 0 ? new Answer
                    {
                        IsActive = true,
                        Id = 0
                    } : _answerService.GetAnswerById(AnswerId).Result,
                      QuestionGroup = _.GetAllActiveClasss().Result
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new StudentModel());
            }
        }


        [Route("duzenle/{StudentId}")]
        [HttpPost]
        public JsonResult StudentEdit(int StudentId, StudentModel StudentModel)
        {
            try
            {
                if (StudentId == 0)
                {
                    Core3Base.Infra.Data.Entity.Student Student = new Core3Base.Infra.Data.Entity.Student
                    {
                        Name = StudentModel.Student.Name,
                        Email = StudentModel.Student.Email,
                        ClassId = StudentModel.Student.ClassId,
                        IsActive = StudentModel.Student.IsActive,
                        Surname = StudentModel.Student.Surname
                    };


                    return Json(_studentService.Add(Student).HttpGetResponse());
                }
                else
                {
                    var Answer = _answerService.GetAnswerById(AnswerId).Result;
                    Answer.Responce = AnswerModel;
                    Student.Email = StudentModel.Student.Email;
                    Student.ClassId = StudentModel.Student.ClassId;
                    Student.IsActive = StudentModel.Student.IsActive;
                    Student.Surname = StudentModel.Student.Surname;
                    Student.DateModified = DateTime.Now;

                    return Json(_answerService.Update(Student).HttpGetResponse());


                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }



        [HttpGet]
        public bool StudentActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var AnswerResponse = _answerService.GetAnswerById(id);
                if (AnswerResponse.IsSucceeded)
                {
                    var Answer = AnswerResponse.Result;
                    Answer.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }

                    _answerService.Update(Answer);
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
                        _answerService.Delete(Convert.ToInt32(silinecekler[i]));
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
                _answerService.Delete(id);
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

