using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using FluentAssertions;
using SyncPrototype.Components;
using System.Collections.Generic;
using System.Linq;

namespace SyncPrototypeTests
{
    [TestClass]
    public class TestingTvpSampleRepository
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
        public void SaveCollectionShould_InsertRecords()
        {
            // Arrange
            var samples = SampleBuilder.Many(3);
            foreach (var sample in samples)
            {
                sample.Id = 0;
            }
            repository.Reset();
            repository.All().Should().BeEmpty("this is a precondition to the test being run");

            var tvpRepository = new SingleTvpRepository(repository);

            // Act
            tvpRepository.Save(samples);
            tvpRepository.Finish();

            // Assert
            repository.All().Should().HaveSameCount(samples);
        }

        [TestMethod, TestCategory("Integration")]
        public void SaveCollectionShould_UpdateRecords()
        {
            // Arrange
            var samples = Seed();
            var tvpRepository = new SingleTvpRepository(repository);
            tvpRepository.Save(samples);
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i].Description = "Changed " + i;
            }

            // Act
            tvpRepository.Save(samples);
            tvpRepository.Finish();

            // Assert
            var changed = repository.All().ToArray();
            changed.Should().HaveSameCount(samples);
            for (int i = 0; i < changed.Length; i++)
            {
                changed[i].Description.Should().Be(samples[i].Description, $"the description at index {i} wasn't changed");
            }
        }

        [TestMethod, TestCategory("Integration")]
        public void SaveCollectionShould_DeleteRecords()
        {
            // Arrange
            var samples = Seed();
            var tvpRepository = new SingleTvpRepository(repository);

            // Act
            foreach (var sample in samples)
            {
                tvpRepository.Delete(sample);
            }

            tvpRepository.Finish();

            // Assert
            repository.All().Should().BeEmpty();
        }

        private Sample[] Seed()
        {
            var count = 3;
            var samples = repository.All().ToArray();
            if (!repository.All().Any())
            {
                var repo = new SingleTvpRepository(repository);
                samples = SampleBuilder.Many(count);
                foreach (var sample in samples)
                {
                    sample.Id = 0;
                }

                repo.Save(samples);
                samples = repository.All().ToArray();
            }

            return samples;
        }


    }
}
