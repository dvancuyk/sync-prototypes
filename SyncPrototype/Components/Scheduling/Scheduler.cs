using SyncPrototype.Client;
using SyncPrototype.Components.Scheduling;
using System;
using System.Collections.Generic;

namespace SyncPrototype.Components
{
    /// <summary>
    /// Responsible for determining if something is scheduled to be processed. 
    /// </summary>
    /// <devdoc>
    /// Every component should have a default value assigned to it.
    /// Each component should be overridable to run at will.
    /// Perhaps look at how cron jobs are configured to determine how to run. I.e, every 5 minutes, 10 minutes, 5 * per hour, 1 per day, etc.
    /// 
    /// The current implementation may need to keep track of state to determine if the task has been run.
    /// </devdoc>
    public class Scheduler
    {
        private Dictionary<string, Crontab> scheduledTasks = new Dictionary<string, Crontab>();

        public Scheduler(params KeyValuePair<string, string>[] rules)
        {
            foreach (var rule in rules)
            {
                scheduledTasks.Add(rule.Key, rule.Value);
            }

            var resourceName = typeof(FINTRX).Name;
            if (!scheduledTasks.ContainsKey(resourceName))
            {
                scheduledTasks.Add(resourceName, "0 0 * * * "); //FINTRX by default is only run at 12:00 AM
            }
            
            ForgivenessWindow = 10;
        }

        /// <summary>
        /// Retrieves the schedule for a provided resource.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public Crontab this[string resourceName]
        {
            get
            {
                return scheduledTasks.ContainsKey(resourceName)
                    ? scheduledTasks[resourceName]
                    : new Crontab(Crontab.RunContinuously);
            }
        }

        /// <summary>
        /// The variance (in minutes) between when a task is scheduled to run and when it is not allowed to run. By default this is 10 minutes.
        /// </summary>
        /// <remarks>
        /// This is primary for tasks to run on a specific schedule and compensates for those times when the parent process runs an extra long time. 
        /// <br />
        /// For example: say that FINTRX is scheduled to run at midnight, but the process started at 11:59 and a few of the preceding resources took a long time so that when the scheduler
        /// has a request for the FINTRX data, it is now 12:01 AM. If the ForgivenessWindow is set to anything greater than 1, FINTRX will still be processed; otherwise it will be skipped.
        /// </remarks>
        public int ForgivenessWindow { get; set; }

        /// <summary>
        /// Determines if the component should be run now.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsScheduledFor<T>()
        {
            return IsScheduledFor<T>(DateTime.Now);
        }


        /// <summary>
        /// Determines if the component should be run at the provided time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsScheduledFor<T>(DateTime time)
        {
            var type = typeof(T).Name;
            if (scheduledTasks.ContainsKey(type))
            {
                var crontab = scheduledTasks[type];

                var validRange = new DateRange(time.AddMinutes(ForgivenessWindow * -1));
                var scheduledTime = crontab.NextScheduledTime(validRange.StartsOn);

                return validRange.IsBetween(scheduledTime);

            }
            return true;
        }
    }
}
