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


namespace CAIROCrons
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            LogManager.LogFactory = new ConsoleLogFactory();

            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;

            string ConnectionString = //"Server = 127.0.0.1; Database = cron; Uid = min; Pwd = 123";
            ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            OrmLiteConnectionFactory DbFactory = new OrmLiteConnectionFactory(ConnectionString, MySqlDialect.Provider);


            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
            using (IDbConnection dbConn = ConnectionString.OpenDbConnection())
            {
                const bool overwrite = false;
                dbConn.CreateTables(overwrite, typeof(Post));
            }
        }
    }
}