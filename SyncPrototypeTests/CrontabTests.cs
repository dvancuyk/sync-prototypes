using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncPrototype.Components.Scheduling;
using FluentAssertions;

namespace SyncPrototypeTests
{
    [TestClass]
    public class CrontabTests
    {
        [TestMethod, TestCategory("Unit")]
        public void NextShould_ReturnSixThirtyOfToday_GivenTodayIsEarlierThanNextTask()
        {
            // Arrange
            var today = DateTime.Today;
            var expected = today.AddHours(18).AddMinutes(30);
            var crontab = new Crontab("30 18 * * *"); // 6:30 PM

            // Act
            var next = crontab.NextScheduledTime(today);

            // Assert
            next.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void NextShould_ReturnSixThirtyOfTomorrow_GivenCurrentDateIsAfterScheduledTaskForToday()
        {
            // Arrange
            var today = DateTime.Today;
            var expected = today
                .AddDays(1)
                .AddHours(18)
                .AddMinutes(30);
            var crontab = new Crontab("30 18 * * *"); // 6:30 PM

            // Act
            var next = crontab.NextScheduledTime(DateTime.Today.AddHours(23));

            // Assert
            next.Should().Be(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void NextShould_ReturnNow_GivenContinuousConfiguration()
        {
            // Arrange
            var now = DateTime.Now;
            var crontab = new Crontab("* * * * *"); // 6:30 PM

            // Act
            var next = crontab.NextScheduledTime(now);

            // Assert
            next.Should().Be(now);
        }
    }
}
