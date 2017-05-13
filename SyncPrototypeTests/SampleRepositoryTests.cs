using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototypeTests
{
    [TestClass]
    public class SampleRepositoryTests
    {
        private SampleRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new SampleRepository(new SqlConnectionFactory());
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

            // Assert
            var saved = repository.All().First();
            saved.Description.Should().Be(sample.Description);
        }
    }
}
