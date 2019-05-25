using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using FluentAssertions;
using Mongo2Go;
using MongoDbEventing;
using Xunit;
using static Tests.TestEvents.EventFactory;

namespace Tests
{
    public sealed class MongoDbEventStoreIntegrationTests : IDisposable
    {
        private readonly MongoDbRunner _runner = MongoDbRunner.Start();

        private readonly IEventStore _eventStore;

        public MongoDbEventStoreIntegrationTests()
        {
            _eventStore = new EventStore(new MongoDbEventStoreContext(
                _runner.ConnectionString,
                "TestDatabase",
                "TestCollection"));
        }

        [Fact]
        public void first_test()
        {
            _eventStore.Append(new List<IDomainEvent> {Customer1Created, Customer1AddressChanged});
            _eventStore.Append(new List<IDomainEvent> {Customer2Created, Customer2AddressChanged});

            var allEvents = _eventStore.LoadAll().ToList();
            
            allEvents.Should().BeEquivalentTo(Customer1Created, Customer1AddressChanged, Customer2Created, Customer2AddressChanged);
        }
        

        public void Dispose()
        {
            _runner.Dispose();
        }
    }
}