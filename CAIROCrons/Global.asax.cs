using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


 
using ServiceStack.OrmLite.SqlServer;
using ServiceStackOrmliteMvc.Helpers;
using ServiceStack.OrmLite;
using ServiceStack.Logging;
using CAIROCrons.Models;
using System.Data;
using System.Configuration;
using CAIROCrons.Services;
using Quartz.Impl.AdoJobStore.Common;


namespace CAIROCrons
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class CAIROCronsApp : System.Web.HttpApplication
    {
        public static int counter = 0;
     
        public static string DBConn = null;
        public static string KeepAliveUri = null;
        public QuartzScheduler quartz = new QuartzScheduler();
        protected void Application_Start()
        {

            
            AreaRegistration.RegisterAllAreas();
            counter = 0;
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
    
            DBConn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            InitDB();
          

            string PingUri = ConfigurationManager.AppSettings["PingServer"];
            string[] arg = PingUri.Split(new char[] { ',' });
             
            quartz.StartUp();
            
        }
 

        protected void Application_Stop()
        {
            if (this.quartz != null)
            {
                quartz.Shutdown();
            }
        }



        void InitDB()
        {
            LogManager.LogFactory = new ConsoleLogFactory();


            if (DBConn.StartsWith("Data Source="))
            { 
                OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
                OrmLiteConnectionFactory DbFactory = new OrmLiteConnectionFactory(DBConn, SqlServerDialect.Provider);
            }
              
            else
            { 
                OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
                OrmLiteConnectionFactory DbFactory = new OrmLiteConnectionFactory(DBConn, MySqlDialect.Provider);
            }


            //

             
            using (IDbConnection dbConn = DBConn.OpenDbConnection())
            {
                const bool overwrite = false;
                dbConn.CreateTables(overwrite, typeof(Post));
                dbConn.CreateTables(overwrite, typeof(DBLogger));
                dbConn.CreateTables(overwrite, typeof(Holidays));
                //HolidaysServices srv = new HolidaysServices();
                //srv.InitDB();
                //srv = null;
            }
        }
    }
}




/*


  <log4net>
    <appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender">
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%d [%t] %-5p %l - %m%n" />
      </layout>
    </appender>
    <appender name="EventLogAppender" type="log4net.Appender.EventLogAppender">
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%d [%t] %-5p %l - %m%n" />
      </layout>
    </appender>

    <appender name="FileAppender" type="log4net.Appender.FileAppender">
      <file value="logs\log-file.txt" />
      <appendToFile value="true" />
      <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger [%property{NDC}] - %message%newline" />
      </layout>
    </appender>
    
    <root>
      <level value="INFO" />
      <appender-ref ref="ConsoleAppender" />
      <!-- uncomment to enable event log appending -->
      <!-- <appender-ref ref="EventLogAppender" /> -->
    </root>
  </log4net>

*/