using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace CAIROCrons.Services
{
     
    public class SchedulerServices : IDisposable
    {
        /// <summary>
        /// Determines the status fo the Scheduler
        /// </summary>        
        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        private bool _Cancelled = false;


        /// <summary>
        /// The frequency of checks against hte POP3 box are 
        /// performed in Seconds.
        /// </summary>
        private int CheckFrequency = 180;

      

      //--debug  AutoResetEvent WaitHandle = new AutoResetEvent(false);

        object SyncLock = new Object();

        public SchedulerServices()
        {

        }

        /// <summary>
        /// Starts the background thread processing       
        /// </summary>
        /// <param name="CheckFrequency">Frequency that checks are performed in seconds</param>
        public void Start(int checkFrequency)
        {
           
            // *** Ensure that any waiting instances are shut down
            //this.WaitHandle.Set();

            this.CheckFrequency = checkFrequency;
            this.Cancelled = false;

            Thread t = new Thread(Run);
            t.Start();
        }

        /// <summary>
        /// Causes the processing to stop. If the operation is still
        /// active it will stop after the current message processing
        /// completes
        /// </summary>
        public void Stop()
        {
            lock (this.SyncLock)
            {
                if (Cancelled)
                    return;

                this.Cancelled = true;
             
            }
        }

        /// <summary>
        /// Runs the actual processing loop by checking the mail box
        /// </summary>
        private void Run()
        {
            DBLoggerService dblog = new DBLoggerService();

            dblog.Log("Start up");
           
            while (!Cancelled)
            {             
                Thread.Sleep(1000*this.CheckFrequency);
                dblog.Log(null);
                System.GC.Collect();
            }
            dblog.Log("Shutting down");
            
        }
 


        #region IDisposable Members

        public void Dispose()
        {
            this.Stop();
        }

        #endregion
    }
}