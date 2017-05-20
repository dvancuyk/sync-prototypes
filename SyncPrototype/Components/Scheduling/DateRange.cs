using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Components.Scheduling
{
    public class DateRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="firstDate">The date where.</param>
        /// <param name="secondDate">The ends on.</param>
        public DateRange(DateTime firstDate, DateTime secondDate)
        {
            var startsOn = secondDate;
            var endsOn = firstDate;
            if (firstDate <= secondDate)
            {
                startsOn = firstDate;
                endsOn = secondDate;
            }

            StartsOn = startsOn;
            EndsOn = endsOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="date">An initial date which represents one end of the date range.</param>
        /// <param name="days">The number of days between the provided date and the second date for the range.</param>
        public DateRange(DateTime date, int days)
            : this(date, date.AddDays(days))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="startsOn">The starts on.</param>
        /// <remarks>This constructor creates a date range spanning from the provided date to the current date.</remarks>
        public DateRange(DateTime startsOn)
            : this(startsOn, DateTime.Now)
        {

        }

        public TimeSpan Range => EndsOn - StartsOn;

        /// <summary>
        /// Gets the date range which spans from 12:00 AM to 11:59 PM of the current date.
        /// </summary>
        /// <value>
        /// The today.
        /// </value>
        public static DateRange Today => new DateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddMinutes(-1));

        /// <summary>
        /// Gets the date which represents the initial point of the duration.
        /// </summary>
        /// <value>
        /// The starts on.
        /// </value>
        public DateTime StartsOn { get; }

        /// <summary>
        /// Gets the date which represents the terminating point for the duration.
        /// </summary>
        /// <value>
        /// The ends on.
        /// </value>
        public DateTime EndsOn { get; }

        /// <summary>
        /// Determines whether the candidate parameter falls within the range specified by this instance.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns></returns>
        public bool IsBetween(DateTime candidate)
        {
            var notBefore = candidate >= StartsOn;
            var notAfter = candidate < EndsOn;

            return notBefore && notAfter;
        }
    }
}
