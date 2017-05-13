﻿using System;
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

            var tvpRepository = new TvpSampleRepository(repository);

            // Act
            tvpRepository.Save(samples);

            // Assert
            repository.All().Should().HaveSameCount(samples);
        }

        [TestMethod, TestCategory("Integration")]
        public void SaveCollectionShould_UpdateRecords()
        {
            // Arrange
            var samples = Seed();
            var tvpRepository = new TvpSampleRepository(repository);
            tvpRepository.Save(samples);
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i].Description = "Changed " + i;
            }

            // Act
            tvpRepository.Save(samples);

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
            var tvpRepository = new TvpSampleRepository(repository);

            // Act
            tvpRepository.Save(new Sample[0]);

            // Assert
            repository.All().Should().BeEmpty();
        }

        private Sample[] Seed()
        {
            var count = 3;
            var samples = repository.All().ToArray();
            if (!repository.All().Any())
            {
                var repo = new TvpSampleRepository(repository);
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
