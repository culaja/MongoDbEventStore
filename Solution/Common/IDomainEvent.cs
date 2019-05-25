using System;

namespace Common
{
    public interface IDomainEvent : IMessage
    {
        /// <summary>
        /// Represents the ID of aggregate which is source of event.
        /// </summary>
        Guid AggregateRootId { get; }

        /// <summary>
        /// Represents topic which can be used for easy filtering in event store for specific aggregates.
        /// </summary>
        string AggregateTopicName { get; }

        /// <summary>
        /// Specific version of aggregate when this event is created.
        /// </summary>
        ulong Version { get; }

        /// <summary>
        /// Timestamp when event was for specific aggregate root.
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Can be called just once when version is not set.
        /// </summary>
        IDomainEvent SetVersion(ulong version);

        /// <summary>
        /// Can be called just once when timestamp is not set.
        /// </summary>
        IDomainEvent SetTimestamp(DateTime timestamp);
    }
}