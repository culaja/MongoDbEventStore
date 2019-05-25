using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public Guid AggregateRootId { get; }
        
        public string AggregateTopicName { get; }

        [JsonIgnore]
        public ulong Version { get; private set; }

        [JsonIgnore]
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateRootId, string aggregateTopicName)
        {
            AggregateRootId = aggregateRootId;
            AggregateTopicName = aggregateTopicName;
        }

        public IDomainEvent SetVersion(ulong version)
        {
            if (Version != 0) throw new InvalidOperationException($"Version already set to {Version} and you want to set it to {version}");
            Version = version;
            return this;
        }

        public IDomainEvent SetTimestamp(DateTime timestamp)
        {
            if (Timestamp != default(DateTime)) throw new InvalidOperationException($"Timestamp already set to {Timestamp} and you want to set it to {timestamp}");
            Timestamp = timestamp;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateTopicName;
        }
    }
}