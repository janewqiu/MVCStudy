using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;



namespace CAIROCrons.Models
{

     [Alias("DBLogger")]
    public class DBLogger   
    {
         public DBLogger()
         {
             LogTime = DateTime.Now;
         }

     

        [AutoIncrement]
        [Alias("LoggerUID")]
        public int UId { get; set; }

        public DateTime LogTime { get; set; }

        //[Index(Unique = true)]
        [StringLength(150)]
        public string Text { get; set; }

     
         
    }
}
