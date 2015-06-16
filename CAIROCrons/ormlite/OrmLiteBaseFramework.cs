using System;
using ServiceStack.DataAnnotations;
using System.IO;
using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.Logging;

namespace ServiceStackMvc.Services
{
  

    public class OrmLiteBaseFramework
    {
        protected virtual string ConnectionString { get; set; }

        private OrmLiteConnectionFactory DbFactory;


        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public IDbConnection InMemoryDbConnection { get; set; }

        public virtual IDbConnection OpenDbConnection(string connString = null)
        {
            connString = connString ?? ConnectionString;
            return connString.OpenDbConnection();
        }
    }

    //public class OrmLiteTestBase
    //{
    //    protected virtual string ConnectionString { get; set; }

    //    [TestFixtureSetUp]
    //    public void TestFixtureSetUp()
    //    {
    //        LogManager.LogFactory = new ConsoleLogFactory();

    //        OrmLiteConfig.DialectProvider = MySqlDialectProvider.Instance;
    //        ConnectionString = ConfigurationManager.ConnectionStrings["testDb"].ConnectionString;
    //    }

   

  
    //}
}
