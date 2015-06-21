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
    public class HolidaysServices
    {
        public HolidaysServices()
        {
        }

        public void Insert(Holidays row)
        {
            using (IDbConnection dbConn = CAIROCronsApp.DBConn.OpenDbConnection())
            {
                dbConn.Insert(row);
            }
        }

    


        //
        public void Setup(string text, string year, string Provincial)
        {
            string[] line = text.Split(new char[] { '\n', '\r' });
            
            foreach(string row in line)
            {
                if (string.IsNullOrEmpty(row))
                    continue;

                Holidays day = new Holidays();
                string[] column = row.Split(new char[] { '|' });
                day.Title = column[0];
                day.Holiday = DateTime.Parse(column[1] );
                day.Provincial = Provincial;
                this.Insert(day);
            }

        }
        
        public void InitDB()
        {
            // create table. init values

            string data =@"New Year's Day|1/1/2015
Family Day|2/16/2015
Good Friday|4/3/2015
Easter Monday *|4/6/2015
Victoria Day|5/18/2015
Canada Day|7/1/2015
Civic Holiday|8/3/2015
Labour Day|9/7/2015
Thanksgiving Day|10/12/2015
Christmas Day|12/25/2015
Boxing Day|12/26/2015";
            this.Setup(data, "2015", "Ontario");

        }
        //public void Edit(Post post)
        //{
        //    using (IDbConnection dbConn = CAIROCronsApp.DBConn.OpenDbConnection())
        //    { 
        //        dbConn.Update(post);
        //    }
        //}
        
        //public void Delete(Guid Id)
        //{
        //    using (IDbConnection dbConn = CAIROCronsApp.DBConn.OpenDbConnection())
        //    {
        //        dbConn.DeleteById<Post>(Id);
        //    }
        //}
        
        //public IList<Post> GetPosts()
        //{
        //    using (IDbConnection dbConn = CAIROCronsApp.DBConn.OpenDbConnection())
        //    {
 
        //        var res = dbConn.Select<Post>();
        //        return res;
 
        //    }
        //}
        
        //public Post GetPost(Guid id)
        //{
        //    using (IDbConnection dbConn = CAIROCronsApp.DBConn.OpenDbConnection())
        //    {
        //        return dbConn.Select<Post>().Where(x => x.Id == id).FirstOrDefault();
        //    }
        //}

        //public Post GetPost(string url)
        //{
        //    using (IDbConnection dbConn = CAIROCronsApp.DBConn.OpenDbConnection())
        //    {
        //        return dbConn.Select<Post>().Where(x => x.Url == url).FirstOrDefault();
        //    }
        //}
    }
}