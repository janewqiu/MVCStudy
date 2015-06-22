using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System;
using System.Threading;
using Common.Logging;
using Quartz.Impl;
using Quartz;

namespace CAIROCrons.Services
{
    public class QuartzScheduler
    {
        ILog log = LogManager.GetLogger(typeof(QuartzScheduler));
        IScheduler sched;
        public void StartUp()
        {

            log.Info("------- StartUp Initializing ----------------------");

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();

            log.Info("-------  StartUp Initialization Complete -----------");


            // computer a time that is on the next round minute
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);


            sched.Start();
        }




        public void Shutdown()
        {
            // shut down the scheduler
            log.Info("------- Shutting Down ---------------------");
            sched.Shutdown(true);
            log.Info("------- Shutdown Complete -----------------");
        }
    }
}