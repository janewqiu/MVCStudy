using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;



namespace CAIROCrons.Models
{

    public class TaskDefine
    {
        public string TaskType;
        public string Permission;
    }

    public class EmailTaskDefine:TaskDefine
    {
        public string SendFrom ;
        public string SendTo;
        public string CCUser;
        public string Attachment;
        public string StringTemplate;
    }

    public class SQLTaskDefine : TaskDefine
    {
        public string ConnectionString;
        public string SpecUser;
        public string SpecPass;
        public bool IsNTLM;
        public byte[] SQLQueryBody;  // zip, binary
        public byte[] ExcelTemplate; // zip, binary
    }

    public class FTPTaskDefine : TaskDefine
    {
        public string ftpserver;
        public string ftpuser;
        public string ftppassword;
    }

    public enum TaskTypes
    {
        Email, SQL, FTP
    }

    [Alias("TaskSetting")]
    public class TaskSetting
    {
        public TaskSetting()
        {
            if (this.Id == Guid.Empty) this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }


        public DateTime Date { get; set; }

        public string CreatedBy { get; set; }

        /// <summary>
        /// the server, database, username/password, ftp server name ... 
        /// all keep in key/value model inside with json
        /// </summary>
        public string ConfigDetails { get; set; }

        public string SQLQuery { get; set; }

        public string EmailTemplate { get; set; }

 
        public string Descriptions { get; set; }
 

        public IList<Comment> ExecHistory { get; set; }

        public int TotalExecuted { get; set; }

    }


    public class Dashboard
    {
        public string ServerTime { get; set; }
        public IList<TaskList> task { get; set; }
    }

    [Alias("TaskList")]
    public class TaskList
    {
        public TaskList()
        {
            if (this.Id == Guid.Empty) this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }


        public DateTime ExecuteDate { get; set; }

        
        public IList<Comment> Comments { get; set; }


    }

}
