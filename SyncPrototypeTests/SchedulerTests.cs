using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using System;
using System.Collections.Generic;

namespace SyncPrototypeTests
{
    [TestClass]
    public class SchedulerTests
    {
        private Scheduler Create()
        {
            return new Scheduler();
        }

        [TestMethod, TestCategory("Unit")]
        public void IsScheduledShould_ReturnFalse_GivenTaskIsScheduledToLater()
        {
            // Arrange
            Scheduler scheduler = Create();

            // Act
            var isScheduled = scheduler.IsScheduledFor<FINTRX>(DateTime.Today.AddHours(6));

            // Assert
            isScheduled.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void IsScheduledShould_ReturnTrue_GivenResourceShouldBeRunContinuously()
        {
            // Arrange
            Scheduler scheduler = Create();

            // Act
            var isScheduled = scheduler.IsScheduledFor<Smpl>();

            // Assert
            isScheduled.Should().BeTrue();
        }

        [TestMethod, TestCategory("Unit")]
        public void ScheduleConstructorShould_OverrideSchedule_GivenOverrideValueProvidedDuringConstruction()
        {
            // Arrange
            var resource = typeof(FINTRX).Name;
            var schedule = "15 11 * * *";
            var newRule = new KeyValuePair<string, string>(resource, schedule);

            // Act
            var scheduler = new Scheduler(newRule);

            // Assert
            var registeredRule = scheduler[resource];
            registeredRule.ToString().Should().Be(schedule);
        }
    }
}
