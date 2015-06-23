using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAIROCrons.Services.jobs
{
    public class KeepServerAliveJob : IJob
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(KeepServerAliveJob));

            /// <summary>
            /// Called by the <see cref="IScheduler" /> when a
            /// <see cref="ITrigger" /> fires that is associated with
            /// the <see cref="IJob" />.
            /// </summary>
            public virtual void Execute(IJobExecutionContext context)
            {
                // This job simply prints out its job name and the
                // date and time that it is running
                JobKey jobKey = context.JobDetail.Key;
                log.DebugFormat("SimpleJob says: {0} executing at {1}", jobKey, DateTime.Now.ToString("r"));
            }
        }
    
}