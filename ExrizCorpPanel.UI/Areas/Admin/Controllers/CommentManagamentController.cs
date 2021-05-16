using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExrizCorpPanel.UI.Areas.Admin.Models.CustomModels.RequestTypes;
using ExrizCorpPanel.Core.Infrastructure;
using ExrizCorpPanel.Data.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExrizCorpPanel.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CommentManagamentController : Controller
    {
        private ILanguageRepository _language;
        private ICustomerCommentsRepository _comment;
        private ICommentDetailRepository _detail;

        public CommentManagamentController(ILanguageRepository language, ICustomerCommentsRepository comment, ICommentDetailRepository detail )
        {
            _comment = comment;
            _language = language;
            _detail = detail;
        }
        #region InsertComment
        public IActionResult AddComment()
        {
            var language = _language.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            //liste.Items.a
            TempData["lang"] = liste;

            return View();
        }

        [HttpPost]
        public int PostComment([FromBody]CommentRequest comment)
        {
            var newComment = new CustomerComments();
            newComment.CustomerFirm = comment.CustomerFirm;
            newComment.CustomerName = comment.CustomerName;
            var id=_comment.InsertAndgetId(newComment);
            return id;
        }


        #endregion

        #region UpdateComment
        public IActionResult UpdateComment(int id)
        {
            var language = _language.GetAll();
            var liste = new List<SelectListItem>();
            liste.Add(new SelectListItem { Text = "Lütfen dil seçin", Value = "-1", Selected = true });
            foreach (var item in language)
            {
                liste.Add(new SelectListItem { Text = item.LangName, Value = item.Id.ToString(), Selected = false });
            }
            //liste.Items.a
            TempData["lang"] = liste;

            var selected = _comment.GetById(id);
            return View(selected);
        }

        [HttpPost]
        public IActionResult PostUpdateComment()
        {
            return View();
        }
        #endregion

        #region GetComment

        public IActionResult GetComment()
        {
            var list = _comment.GetAll();
            return View(list);
        }

        #endregion

        #region DeleteComment

        public IActionResult deleteComment(int id)
        {
            var list = _detail.GetMany(x=>x.CustomerCommentId==id);
            foreach (var item in list)
            {
                _detail.Delete(item.Id);
            }
            _detail.Save();
            _comment.Delete(id);
            _comment.Save();
            return RedirectToAction("GetComment","CommentManagament");
        }


        #endregion

        #region InsertCommentDetail
      

        [HttpPost]
        public int addCommentDetail([FromBody]List<CommentDetailRequest> com)
        {
            foreach (var item in com)
            {
                _detail.Insert(new CommentDetail { CustomerCommentId=item.CustomerCommentId,LangId=item.LangId,CustomerComment=item.CustomerComment});
                _detail.Save();
            }
            return 1;
           
        }


        #endregion

        #region UpdateCommentDetail
     
        [HttpPost]
        public int updateCommentDetail([FromBody]List<CommentDetailRequest> com)
        {
            foreach (var item in com)
            {
                var selected = _detail.GetById(item.Id);
                selected.CustomerComment = item.CustomerComment;
                _detail.Update(selected);
                _detail.Save();
            }
            return 1;
        }
        #endregion


        #region OtherActions

        public IActionResult countComment()
        {
            //dil idsi verilecek
            return View();
        }
        public IActionResult countCommentDetail()
        {
            //dil idsi verilecek
            return View();
        }
        #endregion 
    }
}