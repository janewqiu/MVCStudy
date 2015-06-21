using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ServiceStack.OrmLite;
using ServiceStackOrmliteMvc;
using System.Web;
using ServiceStack.DataAnnotations;
using System.IO;
using System.Data;
using ServiceStack.Logging;
using CAIROCrons.Models;


namespace CAIROCrons.Services
{
    public class PostService
    {
        public PostService()
        {
        }
       
        public void Create(Post post)
        {
            post.Comments = new List<Comment>();
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                dbConn.Insert(post);
                HttpContext.Current.Session["Id"] = post.Id;
            }
        }
       
        public void Edit(Post post)
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            { 
                dbConn.Update(post);
            }
        }
        
        public void Delete(Guid Id)
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                dbConn.DeleteById<Post>(Id);
            }
        }
        
        public IList<Post> GetPosts()
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
 
                var res = dbConn.Select<Post>();
                return res;
 
            }
        }
        
        public Post GetPost(Guid id)
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                return dbConn.Select<Post>().Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public Post GetPost(string url)
        {
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                return dbConn.Select<Post>().Where(x => x.Url == url).FirstOrDefault();
            }
        }
    }
}