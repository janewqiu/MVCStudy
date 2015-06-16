using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using ServiceStackMvc;
using System.Web;
using ServiceStack.DataAnnotations;
using System.IO;
using System.Data;
using ServiceStack.Logging;
using CAIROCrons.Models;

namespace CAIROCrons.Services
{
    public class CommentService
    {
        public CommentService()
        {
        }
        public void AddComment(Comment comment)
        {
            // Guid Id = (Guid) HttpContext.Current.Session["Id"];
            Guid Id = Guid.Parse(HttpContext.Current.Request.Form["PostId"]);
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                {
                    Post post = dbConn.Select<Post>(u => u.Id == Id).FirstOrDefault();
                    post.TotalComments++;
                    if (post.Comments == null)
                        post.Comments = new List<Comment>();
                    post.Comments.Add(comment);
                    dbConn.Save(post);
                }
            }
        }
        public void RemoveComment(Guid Id, Guid commentId)
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                Post post = dbConn.Select<Post>(u => u.Id == Id).FirstOrDefault();
                post.TotalComments--;
                Comment comment = post.Comments.Where(x => x.CommentId == commentId).FirstOrDefault();
                post.Comments.Remove(comment);
                dbConn.Save(post);
            }
        }
        public IList<Comment> GetComments(Guid Id, int skip, int limit, int totalComments)
        {
            List<Comment> c = new List<Comment>();
            return c;
        }
        public int GetTotalComments(Guid Id)
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                return dbConn.Select<Comment>().Count(u => u.Id == Id);
            }
        }
    }
}
