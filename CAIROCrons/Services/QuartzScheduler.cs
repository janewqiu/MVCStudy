using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using System.Threading;
using Common.Logging;
using Quartz.Impl;
using Quartz;
using CAIROCrons.Services.jobs;

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

            ResumeJobsFromDB();
            sched.Start();
            
        }

        void ResumeJobsFromDB()
        {
            try
            {
                // job5 will run once, five minutes in the future
                var job = JobBuilder.Create<KeepServerAliveJob>()
                    .WithIdentity("job15", "group1")
                    .Build();

                var trigger = (ISimpleTrigger)TriggerBuilder.Create()
                                                .WithIdentity("Repeater", "group1")
                                                 .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(20))
                                               // .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second))
                                                
                                                .Build();

                var ft = sched.ScheduleJob(job, trigger);
                log.Info(job.Key +
                         " will run at: " + ft +
                         " and repeat: " + trigger.RepeatCount +
                         " times, every " + trigger.RepeatInterval.TotalSeconds + " seconds");
            }
            catch  
            {
                
            }

        }



        public void Shutdown()
        {
            log.Info("------- Shutting Down ---------------------");

            sched.Shutdown(true);

            log.Info("------- Shutdown Complete -----------------");

            // display some stats about the schedule that just ran
            SchedulerMetaData metaData = sched.GetMetaData();
            log.Info(string.Format("Executed {0} jobs.", metaData.NumberOfJobsExecuted));
        }
    }
}