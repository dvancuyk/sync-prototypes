using System;

namespace SyncPrototype.Components.Scheduling
{
    /// <summary>
    /// Representation of a scheduled cron tab used for scheduling!
    /// </summary>
    /// <devdoc>
    /// This only supports minutes and hours for now. Eventually we can flesh out the design to include more robust functionality but minutes and hours meets our requirements and
    /// thus is only supported for time sake.
    /// </devdoc>
    public class Crontab
    {
        internal const string RunContinuously = "* * * * *";
        private string description;

        private int minutes = 0;
        private int hours = 0;

        public Crontab(string crontab)
        {
            this.description = crontab;
            var fields = crontab.Split(' ');

            if(fields.Length > 0)
            {
                int.TryParse(fields[0], out minutes);
            }

            if (fields.Length > 1)
            {
                int.TryParse(fields[1], out hours);
            }
        }

        /// <summary>
        /// Returns the next scheduled time from this point in time.
        /// </summary>
        /// <returns></returns>
        public DateTime NextScheduledTime()
        {
            return NextScheduledTime(DateTime.Now);
        }

        /// <summary>
        /// Returns the next scheduled time from the provided point in time.
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public DateTime NextScheduledTime(DateTime from)
        {
            // This gives us our schedule
            var schedule = DateTime
                .Today
                .Add(new TimeSpan(hours, minutes, 0));

            if (description.Equals(RunContinuously))
                return from;

            return schedule > from
                ? schedule
                : schedule.AddDays(1);
        }

        public override string ToString()
        {
            return description;
        }

        /// <summary>
        /// Allows implicit conversions from a string to a Crontab.
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Crontab(string value)
        {
            return new Crontab(value);
        }
    }
}
