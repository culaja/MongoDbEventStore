using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbEventing
{
    public sealed class PersistedEvent
    {
        public ObjectId Id { get; set; }
        
        public Guid AggregateId { get; set; }

        public string AggregateTopicName { get; set; }

        public ulong AggregateVersion { get; set; }

        [BsonDateTimeOptions(Representation = BsonType.Document)]
        public DateTime Timestamp { get; set; }

        public IReadOnlyList<string> Payload { get; set; }

        private PersistedEvent(IReadOnlyList<IDomainEvent> domainEvents)
        {
            Id = ObjectId.GenerateNewId();
            AggregateId = domainEvents[0].AggregateRootId;
            AggregateTopicName = domainEvents[0].AggregateTopicName;
            AggregateVersion = domainEvents[0].Version;
            Timestamp = domainEvents[0].Timestamp;
            Payload = domainEvents.Select(e => e.Serialize()).ToList();
        }
        
        public static PersistedEvent PersistedEventFrom(IReadOnlyList<IDomainEvent> domainEvents)
            => new PersistedEvent(domainEvents);

        public IReadOnlyList<IDomainEvent> ToDomainEvents() => Payload
            .Select(serializedEvent => ((IDomainEvent) serializedEvent.Deserialize())
                .SetVersion(AggregateVersion)
                .SetTimestamp(Timestamp))
            .ToList();
    }
}