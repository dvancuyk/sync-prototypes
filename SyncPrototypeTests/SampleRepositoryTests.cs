using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using System;
using System.Linq;

namespace SyncPrototypeTests
{
    [TestClass]
    public class SampleRepositoryTests
    {
        private MultipleTvpRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new MultipleTvpRepository(new SqlConnectionFactory());
        }

        [TestCleanup]
        public void Cleanup()
        {
            repository.Dispose();
        }


        [TestMethod, TestCategory("Integration")]
        public void SaveShould_InsertRecord()
        {
            // Arrange
            repository.Reset();
            var sample = SampleBuilder.Single(0);

            // Act
            repository.Save(sample);
            repository.Finish();

            // Assert
            repository.All().Should().HaveCount(1);
        }

        [TestMethod, TestCategory("Integration")]
        public void SaveShould_UpdateRecord()
        {
            // Arrange
            var sample = repository.All().FirstOrDefault();
            if(sample == null)
            {
                repository.Save(SampleBuilder.Single(0));
                sample = repository.All().First();
            }
            sample.Description = "Updated at " + DateTime.Now.ToLongTimeString();

            // Act
            repository.Save(sample);
            repository.Finish();

            // Assert
            var saved = repository.All().First(s => s.Id == sample.Id);
            saved.Description.Should().Be(sample.Description);
        }
    }
}
