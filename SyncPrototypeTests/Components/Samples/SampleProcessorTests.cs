using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Components.Samples;
using SyncPrototype.Connect;
using System;
using System.Linq;

namespace SyncPrototypeTests.Components.Samples
{
    [TestClass]
    public class SampleProcessorTests
    {
        [TestMethod, TestCategory("Unit")]
        public void ProcessShould_SaveRecord_GivenConnectExistsAndClientChanged()
        {
            // Arrange
            var clientRepository = Substitute.For<IRepository<Smpl>>();
            var connectRepository = Substitute.For<IRepository<Sample>>();
            var mapper = new SampleConnectMapper();
            var source = SyncPrototype.Db.SmpleBuilder.Single(1);
            var candidate = mapper.Convert(source);

            source.Description = Guid.NewGuid().ToString(); // Here is our change
            clientRepository.All().Returns(new[] { source });
            connectRepository.All().Returns(new[] { candidate });

            var processor = new SampleProcessor(clientRepository, connectRepository);

            // Act
            processor.Process();

            // Assert
            connectRepository
                .Received(1)
                .Save(candidate);
            candidate.Description.Should().Be(source.Description);
        }
    }
}
