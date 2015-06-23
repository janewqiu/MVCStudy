using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAIROCrons.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ServiceStack MVC Blog!";
       //     System.Diagnostics.Debug.WriteLine("Hi, index");

            Log.Info("hello log" + DateTime.Now);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}


/*
https://cmatskas.com/an-introduction-to-common-logging-api-2/
 * <configSections>  
    <sectionGroup name="common">
      <section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging"/>
    </sectionGroup>
  </configSections>
  <common>
    <logging>
      <factoryAdapter type="Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter, Common.Logging">
        <arg key="level" value="ALL"/>
        <arg key="showLogName" value="true"/>
        <arg key="showDataTime" value="true"/>
        <arg key="dateTimeFormat" value="yyyy/MM/dd HH:mm:ss:fff"/>
      </factoryAdapter>
    </logging>
  </common>
 
*/