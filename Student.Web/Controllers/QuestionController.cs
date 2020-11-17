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
    [Route("sorular")]

    public class QuestionController : Controller
    {
        private IQuestionService _QuestionService;

        public QuestionController(IQuestionService QuestionService)
        {
            _QuestionService = QuestionService;
        }

        [Route("liste")]
        [HttpGet]
        public ActionResult QuestionList()
        {
            var model = new QuestionModel
            {
                Questions = new List<Core3Base.Infra.Data.Entity.Question>()
            };
            return View(model);
        }
        [Route("Question-query")]
        [HttpPost]
        public JsonResult QuestionListQuery(DataTablesModel.DataTableAjaxPostModel model)
        {
            try
            {
                var data = _QuestionService.GetAllForDatatables(model);
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
        public ActionResult QuestionCreate()
        {
            return RedirectToAction("QuestionEdit", new { QuestionId = 0 });
        }


        [Route("duzenle/{QuestionId}")]
        [HttpGet]
        public ActionResult QuestionEdit(int QuestionId)
        {
            try
            {
                var model = new QuestionModel
                {
                    Question = QuestionId == 0 ? new Core3Base.Infra.Data.Entity.Question
                    {
                        IsActive = true,
                        Id = 0
                    } : _QuestionService.GetQuestionById(QuestionId).Result,
                };
                return View(model);
            }
            catch (Exception e)
            {

                return View(new QuestionModel());
            }
        }


        [Route("duzenle/{QuestionId}")]
        [HttpPost]
        public JsonResult QuestionEdit(int QuestionId, QuestionModel QuestionModel)
        {
            try
            {
                if (QuestionId == 0)
                {
                    Core3Base.Infra.Data.Entity.Question Question = new Core3Base.Infra.Data.Entity.Question
                    {

                         Content = QuestionModel.Question.Content,
                        IsActive = QuestionModel.Question.IsActive,
                    };

                    return Json(_QuestionService.Add(Question).HttpGetResponse());
                }
                else
                {
                    var Question = _QuestionService.GetQuestionById(QuestionId).Result;
                    Question.Content = QuestionModel.Question.Content;
                    Question.IsActive = QuestionModel.Question.IsActive; 
                    return Json(_QuestionService.Update(Question).HttpGetResponse());

                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public bool QuestionActiveChange(int id, bool active)
        {
            try
            {
                string sonuc = "";
                var QuestionResponse = _QuestionService.GetQuestionById(id);
                if (QuestionResponse.IsSucceeded)
                {
                    var Question = QuestionResponse.Result;
                    Question.IsActive = active;
                    if (active)
                    {
                        sonuc = "aktif";
                    }
                    else
                    {
                        sonuc = "pasif";
                    }

                    _QuestionService.Update(Question);
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
                        _QuestionService.Delete(Convert.ToInt32(silinecekler[i]));
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
        public bool QuestionDelete(int id)
        {
            try
            {
                _QuestionService.Delete(id);
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
