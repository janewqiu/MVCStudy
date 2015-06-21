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


namespace CAIROCrons
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class CAIROCronsApp : System.Web.HttpApplication
    {
        public static int counter = 0;
        public SchedulerServices services = null;

        public static string DBConn = null;


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
            services = new SchedulerServices();
            services.Start(1);
         
        }

        protected void Application_Stop()
        {
            if (this.services!=null)
            {
                services.Stop();
                services.Dispose();
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