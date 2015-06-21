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
    public class DBLoggerService
    {
        public DBLoggerService()
        {
        }

        public void Insert(DBLogger post)
        {
             
            using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
            {
                dbConn.Insert(post);
            }
        }


        public void Log(string text)
        {
            DBLogger log = new DBLogger();
            
            long bytes = GC.GetTotalMemory(false);
            if (!string.IsNullOrEmpty(text))
            { 
                log.Text = text; 
            }
            else 
            { 
                log.Text = string.Format("Count:{0} Memory {1}", MvcApplication.counter++, bytes); 
            }

            Insert(log);
        }
       
        //public void Edit(Post post)
        //{
        //    using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
        //    { 
        //        dbConn.Update(post);
        //    }
        //}
        
        //public void Delete(Guid Id)
        //{
        //    using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
        //    {
        //        dbConn.DeleteById<Post>(Id);
        //    }
        //}
        
        //public IList<Post> GetPosts()
        //{
        //    using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
        //    {
 
        //        var res = dbConn.Select<Post>();
        //        return res;
 
        //    }
        //}
        
        //public Post GetPost(Guid id)
        //{
        //    using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
        //    {
        //        return dbConn.Select<Post>().Where(x => x.Id == id).FirstOrDefault();
        //    }
        //}

        //public Post GetPost(string url)
        //{
        //    using (IDbConnection dbConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString.OpenDbConnection())
        //    {
        //        return dbConn.Select<Post>().Where(x => x.Url == url).FirstOrDefault();
        //    }
        //}
    }
}