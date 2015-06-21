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


namespace CAIROCrons.Services
{
    public class SetupServices
    {
        public SetupServices()
        {
        }

        public void CreateDB()
        {

            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
            string ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            //OrmLiteConnectionFactory DbFactory = new OrmLiteConnectionFactory(ConnectionString, MySqlDialect.Provider);
                
            using (IDbConnection dbConn = ConnectionString.OpenDbConnection())
            {
                const bool overwrite = false;
                dbConn.CreateTables(overwrite, typeof(Post));
                dbConn.CreateTables(overwrite, typeof(Post));
            }
        }
    }
}