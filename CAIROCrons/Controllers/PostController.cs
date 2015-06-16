using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack;
using ServiceStackMvc;
 
using ServiceStackMvc.Services;
using ServiceStackMvc.Helpers;
using CAIROCrons.Models;
using CAIROCrons.Services;


namespace CAIROCrons.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly CommentService _commentService;

        public PostController()
        {
            _postService = new PostService();
            _commentService = new CommentService();
        }

        public ActionResult Index()
        {
            return View(_postService.GetPosts());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Post post = new Post();
            return View(post);
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Url = post.Title.GenerateSlug();
                post.Author = User.Identity.Name;
                post.Date = DateTime.Now;

                _postService.Create(post);

               TempData["Id"] = post.Id;

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            return View(_postService.GetPost(id));
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            return View(_postService.GetPost(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(Guid id)
        {
            _postService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Url = post.Title.GenerateSlug();

                _postService.Edit(post);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Detail(Guid id)
        {
            var post = _postService.GetPost(id);
            TempData["Id"] = post.Id;

            ViewBag.TotalComments = post.TotalComments;
            ViewBag.LoadedComments = 5;

            return View(post);
        }

        [HttpPost]
        public ActionResult AddComment( Comment comment)
        {
            
             
            if (ModelState.IsValid)
            {
                var newComment = new Comment()
                                        {
                                            CommentId = Guid.NewGuid() ,
                                            Author = User.Identity.Name,
                                            Date = DateTime.Now,
                                            Detail = comment.Detail,
                                            PostId=comment.PostId 
                                        };

                _commentService.AddComment( newComment);

                 
               
                return Json(
                    new
                        {
                            Result = "ok",
                            CommentHtml = RenderPartialViewToString("Comment", newComment),
                            FormHtml = RenderPartialViewToString("AddComment", new Comment())
                        });
            }

            
            return Json(
                new
                    {
                        Result = "fail",
                        FormHtml = RenderPartialViewToString("AddComment", comment)
                    });
        }

        public ActionResult RemoveComment(Guid Id, Guid commentId)
        {
            _commentService.RemoveComment(Id, commentId);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult CommentList(Guid Id, int skip, int limit, int totalComments)
        {
            ViewBag.TotalComments = totalComments;
            ViewBag.LoadedComments = skip + limit;
            return PartialView(_commentService.GetComments(Id, ViewBag.LoadedComments, limit, totalComments));
        }

        /// <summary>
        /// http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}